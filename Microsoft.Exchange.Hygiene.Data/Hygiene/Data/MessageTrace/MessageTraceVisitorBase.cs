using System;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x0200016F RID: 367
	internal abstract class MessageTraceVisitorBase : IMessageTraceVisitor
	{
		// Token: 0x06000EF9 RID: 3833 RVA: 0x0002E78E File Offset: 0x0002C98E
		public virtual void Visit(MessageTraceEntityBase entity)
		{
		}

		// Token: 0x06000EFA RID: 3834 RVA: 0x0002E790 File Offset: 0x0002C990
		public virtual void Visit(MessageTrace messageTrace)
		{
		}

		// Token: 0x06000EFB RID: 3835 RVA: 0x0002E792 File Offset: 0x0002C992
		public virtual void Visit(MessageClientInformation messageTrace)
		{
		}

		// Token: 0x06000EFC RID: 3836 RVA: 0x0002E794 File Offset: 0x0002C994
		public virtual void Visit(MessageClientInformationProperty messageTrace)
		{
		}

		// Token: 0x06000EFD RID: 3837 RVA: 0x0002E796 File Offset: 0x0002C996
		public virtual void Visit(MessageProperty messageProperty)
		{
		}

		// Token: 0x06000EFE RID: 3838 RVA: 0x0002E798 File Offset: 0x0002C998
		public virtual void Visit(MessageEvent messageEvent)
		{
		}

		// Token: 0x06000EFF RID: 3839 RVA: 0x0002E79A File Offset: 0x0002C99A
		public virtual void Visit(MessageEventProperty messageEventProperty)
		{
		}

		// Token: 0x06000F00 RID: 3840 RVA: 0x0002E79C File Offset: 0x0002C99C
		public virtual void Visit(MessageEventRule messageEventRule)
		{
		}

		// Token: 0x06000F01 RID: 3841 RVA: 0x0002E79E File Offset: 0x0002C99E
		public virtual void Visit(MessageEventRuleProperty messageEventRuleProperty)
		{
		}

		// Token: 0x06000F02 RID: 3842 RVA: 0x0002E7A0 File Offset: 0x0002C9A0
		public virtual void Visit(MessageEventRuleClassification messageEventRuleClassification)
		{
		}

		// Token: 0x06000F03 RID: 3843 RVA: 0x0002E7A2 File Offset: 0x0002C9A2
		public virtual void Visit(MessageEventRuleClassificationProperty messageEventRuleClassificationProperty)
		{
		}

		// Token: 0x06000F04 RID: 3844 RVA: 0x0002E7A4 File Offset: 0x0002C9A4
		public virtual void Visit(MessageEventSourceItem messageEventSourceItem)
		{
		}

		// Token: 0x06000F05 RID: 3845 RVA: 0x0002E7A6 File Offset: 0x0002C9A6
		public virtual void Visit(MessageEventSourceItemProperty messageEventSourceItemProperty)
		{
		}

		// Token: 0x06000F06 RID: 3846 RVA: 0x0002E7A8 File Offset: 0x0002C9A8
		public virtual void Visit(MessageClassification messageClassification)
		{
		}

		// Token: 0x06000F07 RID: 3847 RVA: 0x0002E7AA File Offset: 0x0002C9AA
		public virtual void Visit(MessageClassificationProperty messageClassificationProperty)
		{
		}

		// Token: 0x06000F08 RID: 3848 RVA: 0x0002E7AC File Offset: 0x0002C9AC
		public virtual void Visit(MessageRecipient messageRecipient)
		{
		}

		// Token: 0x06000F09 RID: 3849 RVA: 0x0002E7AE File Offset: 0x0002C9AE
		public virtual void Visit(MessageRecipientProperty messageRecipientProperty)
		{
		}

		// Token: 0x06000F0A RID: 3850 RVA: 0x0002E7B0 File Offset: 0x0002C9B0
		public virtual void Visit(MessageRecipientStatus recipientStatus)
		{
		}

		// Token: 0x06000F0B RID: 3851 RVA: 0x0002E7B2 File Offset: 0x0002C9B2
		public virtual void Visit(MessageRecipientStatusProperty recipientStatusProperty)
		{
		}

		// Token: 0x06000F0C RID: 3852 RVA: 0x0002E7B4 File Offset: 0x0002C9B4
		public virtual void Visit(MessageAction messageAction)
		{
		}

		// Token: 0x06000F0D RID: 3853 RVA: 0x0002E7B6 File Offset: 0x0002C9B6
		public virtual void Visit(MessageActionProperty messageActionProperty)
		{
		}
	}
}
