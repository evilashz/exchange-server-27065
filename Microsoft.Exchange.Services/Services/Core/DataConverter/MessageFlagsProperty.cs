using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x02000164 RID: 356
	internal class MessageFlagsProperty : SimpleProperty
	{
		// Token: 0x06000A11 RID: 2577 RVA: 0x00030E76 File Offset: 0x0002F076
		public MessageFlagsProperty(CommandContext commandContext, BaseConverter baseConverter) : base(commandContext, baseConverter)
		{
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x00030E80 File Offset: 0x0002F080
		public static MessageFlagsProperty CreateIsUnmodifiedCommand(CommandContext commandContext)
		{
			return MessageFlagsProperty.CreateCommand(commandContext, MessageFlags.IsUnmodified);
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x00030E89 File Offset: 0x0002F089
		public static MessageFlagsProperty CreateIsSubmittedCommand(CommandContext commandContext)
		{
			return MessageFlagsProperty.CreateCommand(commandContext, MessageFlags.HasBeenSubmitted);
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x00030E92 File Offset: 0x0002F092
		public static MessageFlagsProperty CreateIsAssociatedCommand(CommandContext commandContext)
		{
			return MessageFlagsProperty.CreateCommand(commandContext, MessageFlags.IsAssociated);
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x00030E9C File Offset: 0x0002F09C
		public static MessageFlagsProperty CreateIsDraftCommand(CommandContext commandContext)
		{
			return MessageFlagsProperty.CreateCommand(commandContext, MessageFlags.IsDraft);
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x00030EA5 File Offset: 0x0002F0A5
		public static MessageFlagsProperty CreateIsFromMeCommand(CommandContext commandContext)
		{
			return MessageFlagsProperty.CreateCommand(commandContext, MessageFlags.IsFromMe);
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x00030EAF File Offset: 0x0002F0AF
		public static MessageFlagsProperty CreateIsResendCommand(CommandContext commandContext)
		{
			return MessageFlagsProperty.CreateCommand(commandContext, MessageFlags.IsResend);
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x00030EBC File Offset: 0x0002F0BC
		private static MessageFlagsProperty CreateCommand(CommandContext commandContext, MessageFlags flag)
		{
			MessageFlagsConverter baseConverter = new MessageFlagsConverter(flag);
			return new MessageFlagsProperty(commandContext, baseConverter);
		}
	}
}
