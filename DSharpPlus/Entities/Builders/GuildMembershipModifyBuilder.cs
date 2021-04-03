﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace DSharpPlus.Entities
{
    /// <summary>
    /// Represents the Membership Screening that should be performed.
    /// </summary>
    public sealed class GuildMembershipModifyBuilder
    {
        /// <summary>
        /// Gets whether membership screening should be enabled for this guild
        /// </summary>
        public Optional<bool> Enabled { get; internal set; }

        /// <summary>
        /// Gets the server description shown in the membership screening form
        /// </summary>
        public Optional<string> Description { get; internal set; }

        /// <summary>
        /// Gets the fields in this membership screening form
        /// </summary>
        public IReadOnlyCollection<DiscordGuildMembershipScreeningField> Fields => this._Fields.Value;

        internal Optional<List<DiscordGuildMembershipScreeningField>> _Fields = new List<DiscordGuildMembershipScreeningField>();

        /// <summary>
        /// Gets the AuditLog Reason for modifing the Membership screening.
        /// </summary>
        public Optional<string> AuditLogReason { get; internal set; }

        /// <summary>
        /// Sets if the Membership screening is enabled.
        /// </summary>
        /// <param name="enabled">The enabled value.</param>
        /// <returns></returns>
        public GuildMembershipModifyBuilder WithEnabled(bool enabled)
        {
            this.Enabled = enabled;

            return this;
        }

        /// <summary>
        /// Sets the desciption of the membership screening form.
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        public GuildMembershipModifyBuilder WithDescription(string description)
        {
            this.Description = description;

            return this;
        }

        /// <summary>
        /// Sets the field that should be in the screening form.
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public GuildMembershipModifyBuilder WithField(DiscordGuildMembershipScreeningField field)
        {
            this._Fields.Value.Add(field);

            return this;
        }

        /// <summary>
        /// Sets the fields that should be in the screening form.
        /// </summary>
        /// <param name="fields"></param>
        /// <returns></returns>
        public GuildMembershipModifyBuilder WithFields(IEnumerable<DiscordGuildMembershipScreeningField> fields)
        {
            this._Fields.Value.AddRange(fields);

            return this;
        }

        /// <summary>
        /// Sets the reason for the Change.
        /// </summary>
        /// <param name="reason">The reason.</param>
        /// <returns></returns>
        public GuildMembershipModifyBuilder WithAuditLogReason(string reason)
        {
            this.AuditLogReason = reason;

            return this;
        }

        /// <summary>
        /// Sends the changes of the membership screening to Discord
        /// </summary>
        /// <param name="guild">The Guild the builder should be executed against.</param>
        /// <returns></returns>
        public async Task<DiscordGuildMembershipScreening> ModifyAsync(DiscordGuild guild)
        {
            return await guild.ModifyMembershipScreeningFormAsync(this).ConfigureAwait(false);
        }

        /// <summary>
        /// Clears the contents of the builder.
        /// </summary>
        public void Clear()
        {
            this.Enabled = Optional.FromNoValue<bool>();
            this.Description = Optional.FromNoValue<string>();
            this._Fields.Value.Clear();
            this.AuditLogReason = Optional.FromNoValue<string>();
        }

        /// <summary>
        /// Performs validation logic to verify all the input is valid before sending it off to discord.
        /// </summary>
        internal void Validate()
        {

        }
    }
}