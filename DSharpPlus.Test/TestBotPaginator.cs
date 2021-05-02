// This file is part of DSharpPlus project.
//
// Copyright (c) 2015 Mike Santiago
// Copyright (c) 2016-2021 DSharpPlus Development Team
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.EventHandling;

namespace DSharpPlus.Test
{
    /// <summary>
    /// An example implementation of the IPaginationRequest interface.
    /// Take a look at the IPaginationRequest interface in DSharpPlus.Interactivity for more information.
    /// </summary>
    public class TestBotPaginator : IPaginationRequest
    {
        private readonly List<Page> pages;
        private readonly TaskCompletionSource<bool> _tcs;
        private readonly CancellationTokenSource _cts;
        private readonly DiscordMessage _msg;
        private int index = 0;
        private readonly PaginationEmojis _emojis;
        private readonly DiscordUser _usr;

        public int PageCount
            => this.pages.Count;

        public TestBotPaginator(DiscordClient client, DiscordUser usr, DiscordMessage msg, List<Page> pages)
        {
            this.pages = pages;
            this._tcs = new TaskCompletionSource<bool>();
            this._cts = new CancellationTokenSource(TimeSpan.FromSeconds(30));
            this._cts.Token.Register(() => this._tcs.TrySetResult(true));
            this._msg = msg;
            this._emojis = new PaginationEmojis();
            this._usr = usr;
        }

        public async Task DoCleanupAsync() => await this._msg.DeleteAsync().ConfigureAwait(false);

        public async Task<PaginationEmojis> GetEmojisAsync()
        {
            await Task.Yield();
            return this._emojis;
        }

        public async Task<DiscordMessage> GetMessageAsync()
        {
            await Task.Yield();
            return this._msg;
        }

        public async Task<Page> GetPageAsync()
        {
            await Task.Yield();
            return this.pages[this.index];
        }

        public async Task<TaskCompletionSource<bool>> GetTaskCompletionSourceAsync()
        {
            await Task.Yield();
            return this._tcs;
        }

        public async Task<DiscordUser> GetUserAsync()
        {
            await Task.Yield();
            return this._usr;
        }

        public async Task NextPageAsync()
        {
            await Task.Yield();

            if (this.index < pages.Count - 1)
                this.index++;
        }

        public async Task PreviousPageAsync()
        {
            await Task.Yield();

            if (this.index > 0)
                this.index--;
        }

        public async Task SkipLeftAsync()
        {
            await Task.Yield();

            this.index = 0;
        }

        public async Task SkipRightAsync()
        {
            await Task.Yield();

            this.index = this.pages.Count - 1;
        }
    }
}
