using System;

namespace Microsoft.Exchange.Management.MailboxTransportSubmission.MapiProbe
{
	// Token: 0x02000050 RID: 80
	internal abstract class MapiSubmitSystemProbeFactory
	{
		// Token: 0x060002EC RID: 748 RVA: 0x0000BCF8 File Offset: 0x00009EF8
		public static MapiSubmitSystemProbeFactory CreateFactory(MapiSubmitSystemProbeFactory.Type type)
		{
			if (type == MapiSubmitSystemProbeFactory.Type.MonitoringSystemProbe)
			{
				return MapiSubmitMonitoringSystemProbeFactory.CreateInstance();
			}
			throw new ArgumentException(string.Format("type {0} is not a valid enum MapiSubmitSystemProbeFactory.Type", type));
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0000BD26 File Offset: 0x00009F26
		public virtual SendMapiMailDefinition MakeSendMapiMailDefinition(string senderEmailAddress, string recipientEmailAddress)
		{
			if (string.IsNullOrEmpty(senderEmailAddress))
			{
				throw new ArgumentNullException("senderEmailAddress");
			}
			if (string.IsNullOrEmpty(recipientEmailAddress))
			{
				recipientEmailAddress = senderEmailAddress;
			}
			return SendMapiMailDefinition.CreateInstance(this.Subject, "This is a Mapi System Probe message that's Submitted to Store so that Mailbox transport Submission service can process it", "IPM.Note.MapiSubmitSystemProbe", true, true, senderEmailAddress, recipientEmailAddress);
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0000BD5F File Offset: 0x00009F5F
		public virtual DeleteMapiMailDefinition MakeDeleteMapiMailDefinition(string senderEmailAddress, string internetMessageId)
		{
			if (string.IsNullOrEmpty(senderEmailAddress))
			{
				throw new ArgumentException("senderEmailAddress is null or empty");
			}
			return DeleteMapiMailDefinition.CreateInstance("IPM.Note.MapiSubmitSystemProbe", senderEmailAddress, internetMessageId);
		}

		// Token: 0x060002EF RID: 751
		public abstract IMapiMessageSubmitter MakeMapiMessageSubmitter();

		// Token: 0x04000115 RID: 277
		private const string Body = "This is a Mapi System Probe message that's Submitted to Store so that Mailbox transport Submission service can process it";

		// Token: 0x04000116 RID: 278
		private const string MessageClass = "IPM.Note.MapiSubmitSystemProbe";

		// Token: 0x04000117 RID: 279
		private const bool DoNotDeliver = true;

		// Token: 0x04000118 RID: 280
		private const bool DeleteAfterSubmit = true;

		// Token: 0x04000119 RID: 281
		private readonly string Subject = string.Format("MapiSubmitSystemProbe_{0}", Guid.NewGuid());

		// Token: 0x02000051 RID: 81
		public enum Type
		{
			// Token: 0x0400011B RID: 283
			MonitoringSystemProbe
		}
	}
}
