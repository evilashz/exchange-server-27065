using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000262 RID: 610
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SyncCommandItem : DisposeTrackableBase, ISyncClientOperation
	{
		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x06001675 RID: 5749 RVA: 0x00088495 File Offset: 0x00086695
		// (set) Token: 0x06001676 RID: 5750 RVA: 0x0008849D File Offset: 0x0008669D
		public bool IsDraft { get; set; }

		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x06001677 RID: 5751 RVA: 0x000884A6 File Offset: 0x000866A6
		// (set) Token: 0x06001678 RID: 5752 RVA: 0x000884AE File Offset: 0x000866AE
		public bool SendEnabled { get; set; }

		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x06001679 RID: 5753 RVA: 0x000884B7 File Offset: 0x000866B7
		// (set) Token: 0x0600167A RID: 5754 RVA: 0x000884BF File Offset: 0x000866BF
		public string UID { get; set; }

		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x0600167B RID: 5755 RVA: 0x000884C8 File Offset: 0x000866C8
		// (set) Token: 0x0600167C RID: 5756 RVA: 0x000884D0 File Offset: 0x000866D0
		public Dictionary<string, string> AddedAttachments { get; set; }

		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x0600167D RID: 5757 RVA: 0x000884D9 File Offset: 0x000866D9
		// (set) Token: 0x0600167E RID: 5758 RVA: 0x000884E1 File Offset: 0x000866E1
		public SyncBase.SyncCommandType CommandType
		{
			get
			{
				return this.commandType;
			}
			set
			{
				this.commandType = value;
			}
		}

		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x0600167F RID: 5759 RVA: 0x000884EA File Offset: 0x000866EA
		// (set) Token: 0x06001680 RID: 5760 RVA: 0x000884F2 File Offset: 0x000866F2
		public ChangeType ChangeType
		{
			get
			{
				return this.changeType;
			}
			set
			{
				this.changeType = value;
			}
		}

		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x06001681 RID: 5761 RVA: 0x000884FB File Offset: 0x000866FB
		// (set) Token: 0x06001682 RID: 5762 RVA: 0x00088503 File Offset: 0x00086703
		public XmlNode XmlNode
		{
			get
			{
				return this.xmlNode;
			}
			set
			{
				this.xmlNode = value;
			}
		}

		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x06001683 RID: 5763 RVA: 0x0008850C File Offset: 0x0008670C
		public ISyncItemId Id
		{
			get
			{
				return this.serverId;
			}
		}

		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x06001684 RID: 5764 RVA: 0x00088514 File Offset: 0x00086714
		// (set) Token: 0x06001685 RID: 5765 RVA: 0x0008851C File Offset: 0x0008671C
		public ISyncItemId ServerId
		{
			get
			{
				return this.serverId;
			}
			set
			{
				this.serverId = value;
			}
		}

		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x06001686 RID: 5766 RVA: 0x00088525 File Offset: 0x00086725
		// (set) Token: 0x06001687 RID: 5767 RVA: 0x0008852D File Offset: 0x0008672D
		public string SyncId
		{
			get
			{
				return this.syncId;
			}
			set
			{
				this.syncId = value;
			}
		}

		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x06001688 RID: 5768 RVA: 0x00088536 File Offset: 0x00086736
		// (set) Token: 0x06001689 RID: 5769 RVA: 0x0008853E File Offset: 0x0008673E
		public ISyncItem Item
		{
			get
			{
				return this.backendItem;
			}
			set
			{
				this.backendItem = value;
			}
		}

		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x0600168A RID: 5770 RVA: 0x00088547 File Offset: 0x00086747
		// (set) Token: 0x0600168B RID: 5771 RVA: 0x0008854F File Offset: 0x0008674F
		public string ClientAddId
		{
			get
			{
				return this.clientId;
			}
			set
			{
				this.clientId = value;
			}
		}

		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x0600168C RID: 5772 RVA: 0x00088558 File Offset: 0x00086758
		// (set) Token: 0x0600168D RID: 5773 RVA: 0x00088560 File Offset: 0x00086760
		public string Status
		{
			get
			{
				return this.status;
			}
			set
			{
				this.status = value;
			}
		}

		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x0600168E RID: 5774 RVA: 0x00088569 File Offset: 0x00086769
		// (set) Token: 0x0600168F RID: 5775 RVA: 0x00088571 File Offset: 0x00086771
		public int?[] ChangeTrackingInformation
		{
			get
			{
				return this.changeTrackingInformation;
			}
			set
			{
				this.changeTrackingInformation = value;
			}
		}

		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x06001690 RID: 5776 RVA: 0x0008857A File Offset: 0x0008677A
		// (set) Token: 0x06001691 RID: 5777 RVA: 0x00088582 File Offset: 0x00086782
		public string ClassType
		{
			get
			{
				return this.classType;
			}
			set
			{
				this.classType = value;
			}
		}

		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x06001692 RID: 5778 RVA: 0x0008858B File Offset: 0x0008678B
		// (set) Token: 0x06001693 RID: 5779 RVA: 0x00088593 File Offset: 0x00086793
		public ConversationId ConversationId
		{
			get
			{
				return this.conversationId;
			}
			set
			{
				this.conversationId = value;
			}
		}

		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x06001694 RID: 5780 RVA: 0x0008859C File Offset: 0x0008679C
		// (set) Token: 0x06001695 RID: 5781 RVA: 0x000885A4 File Offset: 0x000867A4
		public byte[] ConversationIndex
		{
			get
			{
				return this.conversationIndex;
			}
			set
			{
				this.conversationIndex = value;
			}
		}

		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x06001696 RID: 5782 RVA: 0x000885AD File Offset: 0x000867AD
		// (set) Token: 0x06001697 RID: 5783 RVA: 0x000885B5 File Offset: 0x000867B5
		public bool IsMms { get; set; }

		// Token: 0x06001698 RID: 5784 RVA: 0x000885BE File Offset: 0x000867BE
		protected override void InternalDispose(bool isDisposing)
		{
			if (isDisposing && this.backendItem != null)
			{
				this.backendItem.Dispose();
				this.backendItem = null;
			}
		}

		// Token: 0x06001699 RID: 5785 RVA: 0x000885DD File Offset: 0x000867DD
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<SyncCommandItem>(this);
		}

		// Token: 0x04000DF5 RID: 3573
		private SyncBase.SyncCommandType commandType;

		// Token: 0x04000DF6 RID: 3574
		private ChangeType changeType;

		// Token: 0x04000DF7 RID: 3575
		private string clientId;

		// Token: 0x04000DF8 RID: 3576
		private XmlNode xmlNode;

		// Token: 0x04000DF9 RID: 3577
		private ISyncItemId serverId;

		// Token: 0x04000DFA RID: 3578
		private string syncId;

		// Token: 0x04000DFB RID: 3579
		private ISyncItem backendItem;

		// Token: 0x04000DFC RID: 3580
		private string status;

		// Token: 0x04000DFD RID: 3581
		private int?[] changeTrackingInformation;

		// Token: 0x04000DFE RID: 3582
		private string classType;

		// Token: 0x04000DFF RID: 3583
		private ConversationId conversationId;

		// Token: 0x04000E00 RID: 3584
		private byte[] conversationIndex;
	}
}
