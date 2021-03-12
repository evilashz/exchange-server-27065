using System;

namespace Microsoft.Exchange.Management.MailboxTransportSubmission.MapiProbe
{
	// Token: 0x02000054 RID: 84
	internal class MapiSubmitSystemProbeHandler
	{
		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060002FF RID: 767 RVA: 0x0000BF00 File Offset: 0x0000A100
		private MapiSubmitSystemProbeFactory MonitoringFactory
		{
			get
			{
				if (this.monitoringFactory == null)
				{
					this.monitoringFactory = MapiSubmitSystemProbeFactory.CreateFactory(MapiSubmitSystemProbeFactory.Type.MonitoringSystemProbe);
				}
				return this.monitoringFactory;
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000300 RID: 768 RVA: 0x0000BF1C File Offset: 0x0000A11C
		private IMapiMessageSubmitter MapiMessageSubmitter
		{
			get
			{
				if (this.mapiMessageSubmitter == null)
				{
					this.mapiMessageSubmitter = this.MonitoringFactory.MakeMapiMessageSubmitter();
				}
				return this.mapiMessageSubmitter;
			}
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0000BF3D File Offset: 0x0000A13D
		private MapiSubmitSystemProbeHandler()
		{
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0000BF45 File Offset: 0x0000A145
		public static MapiSubmitSystemProbeHandler GetInstance()
		{
			return MapiSubmitSystemProbeHandler.instance;
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000BF4C File Offset: 0x0000A14C
		public Guid SendMapiSubmitSystemProbe(string senderEmailAddress, string recipientEmailAddress, out string internetMessageId)
		{
			SendMapiMailDefinition mapiMailDefinition = this.MonitoringFactory.MakeSendMapiMailDefinition(senderEmailAddress, recipientEmailAddress);
			internetMessageId = null;
			string entryId;
			Guid mbxGuid;
			this.MapiMessageSubmitter.SendMapiMessage(mapiMailDefinition, out entryId, out internetMessageId, out mbxGuid);
			return MapiSubmitSystemProbeHandler.ComputeSystemProbeId(entryId, mbxGuid);
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000BF84 File Offset: 0x0000A184
		public DeletionResult DeleteMessageFromOutbox(string senderEmailAddress, string internetMessageId)
		{
			DeleteMapiMailDefinition deleteMapiMailDefinition = this.MonitoringFactory.MakeDeleteMapiMailDefinition(senderEmailAddress, internetMessageId);
			return this.MapiMessageSubmitter.DeleteMessageFromOutbox(deleteMapiMailDefinition);
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000BFAC File Offset: 0x0000A1AC
		private static Guid ComputeSystemProbeId(string entryId, Guid mbxGuid)
		{
			if (string.IsNullOrEmpty(entryId))
			{
				throw new ArgumentNullException("entryId");
			}
			if (Guid.Empty == mbxGuid)
			{
				throw new ArgumentNullException("mbxGuid");
			}
			string arg = string.Format("{0:X8}", entryId.GetHashCode());
			string text = mbxGuid.ToString();
			int startIndex = text.IndexOf('-');
			return new Guid(string.Format("{0}{1}", arg, text.Substring(startIndex)));
		}

		// Token: 0x0400011F RID: 287
		private static MapiSubmitSystemProbeHandler instance = new MapiSubmitSystemProbeHandler();

		// Token: 0x04000120 RID: 288
		private MapiSubmitSystemProbeFactory monitoringFactory;

		// Token: 0x04000121 RID: 289
		private IMapiMessageSubmitter mapiMessageSubmitter;
	}
}
