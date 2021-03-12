using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020000B1 RID: 177
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class ClientIntentType
	{
		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000920 RID: 2336 RVA: 0x0001FCAE File Offset: 0x0001DEAE
		// (set) Token: 0x06000921 RID: 2337 RVA: 0x0001FCB6 File Offset: 0x0001DEB6
		public ItemIdType ItemId
		{
			get
			{
				return this.itemIdField;
			}
			set
			{
				this.itemIdField = value;
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000922 RID: 2338 RVA: 0x0001FCBF File Offset: 0x0001DEBF
		// (set) Token: 0x06000923 RID: 2339 RVA: 0x0001FCC7 File Offset: 0x0001DEC7
		public int Intent
		{
			get
			{
				return this.intentField;
			}
			set
			{
				this.intentField = value;
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000924 RID: 2340 RVA: 0x0001FCD0 File Offset: 0x0001DED0
		// (set) Token: 0x06000925 RID: 2341 RVA: 0x0001FCD8 File Offset: 0x0001DED8
		public int ItemVersion
		{
			get
			{
				return this.itemVersionField;
			}
			set
			{
				this.itemVersionField = value;
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000926 RID: 2342 RVA: 0x0001FCE1 File Offset: 0x0001DEE1
		// (set) Token: 0x06000927 RID: 2343 RVA: 0x0001FCE9 File Offset: 0x0001DEE9
		public bool WouldRepair
		{
			get
			{
				return this.wouldRepairField;
			}
			set
			{
				this.wouldRepairField = value;
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000928 RID: 2344 RVA: 0x0001FCF2 File Offset: 0x0001DEF2
		// (set) Token: 0x06000929 RID: 2345 RVA: 0x0001FCFA File Offset: 0x0001DEFA
		public ClientIntentMeetingInquiryActionType PredictedAction
		{
			get
			{
				return this.predictedActionField;
			}
			set
			{
				this.predictedActionField = value;
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x0600092A RID: 2346 RVA: 0x0001FD03 File Offset: 0x0001DF03
		// (set) Token: 0x0600092B RID: 2347 RVA: 0x0001FD0B File Offset: 0x0001DF0B
		[XmlIgnore]
		public bool PredictedActionSpecified
		{
			get
			{
				return this.predictedActionFieldSpecified;
			}
			set
			{
				this.predictedActionFieldSpecified = value;
			}
		}

		// Token: 0x0400054A RID: 1354
		private ItemIdType itemIdField;

		// Token: 0x0400054B RID: 1355
		private int intentField;

		// Token: 0x0400054C RID: 1356
		private int itemVersionField;

		// Token: 0x0400054D RID: 1357
		private bool wouldRepairField;

		// Token: 0x0400054E RID: 1358
		private ClientIntentMeetingInquiryActionType predictedActionField;

		// Token: 0x0400054F RID: 1359
		private bool predictedActionFieldSpecified;
	}
}
