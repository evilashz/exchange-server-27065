using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SyncResponse
{
	// Token: 0x020001C1 RID: 449
	[XmlType(TypeName = "Responses", Namespace = "AirSync:")]
	[Serializable]
	public class Responses
	{
		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06000CBE RID: 3262 RVA: 0x0001EED2 File Offset: 0x0001D0D2
		// (set) Token: 0x06000CBF RID: 3263 RVA: 0x0001EEED File Offset: 0x0001D0ED
		[XmlIgnore]
		public ResponsesChangeCollection ChangeCollection
		{
			get
			{
				if (this.internalChangeCollection == null)
				{
					this.internalChangeCollection = new ResponsesChangeCollection();
				}
				return this.internalChangeCollection;
			}
			set
			{
				this.internalChangeCollection = value;
			}
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x06000CC0 RID: 3264 RVA: 0x0001EEF6 File Offset: 0x0001D0F6
		// (set) Token: 0x06000CC1 RID: 3265 RVA: 0x0001EF11 File Offset: 0x0001D111
		[XmlIgnore]
		public ResponsesDeleteCollection DeleteCollection
		{
			get
			{
				if (this.internalDeleteCollection == null)
				{
					this.internalDeleteCollection = new ResponsesDeleteCollection();
				}
				return this.internalDeleteCollection;
			}
			set
			{
				this.internalDeleteCollection = value;
			}
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06000CC2 RID: 3266 RVA: 0x0001EF1A File Offset: 0x0001D11A
		// (set) Token: 0x06000CC3 RID: 3267 RVA: 0x0001EF35 File Offset: 0x0001D135
		[XmlIgnore]
		public ResponsesAddCollection AddCollection
		{
			get
			{
				if (this.internalAddCollection == null)
				{
					this.internalAddCollection = new ResponsesAddCollection();
				}
				return this.internalAddCollection;
			}
			set
			{
				this.internalAddCollection = value;
			}
		}

		// Token: 0x04000728 RID: 1832
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(ResponsesChange), ElementName = "Change", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "AirSync:")]
		public ResponsesChangeCollection internalChangeCollection;

		// Token: 0x04000729 RID: 1833
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(ResponsesDelete), ElementName = "Delete", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "AirSync:")]
		public ResponsesDeleteCollection internalDeleteCollection;

		// Token: 0x0400072A RID: 1834
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(ResponsesAdd), ElementName = "Add", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "AirSync:")]
		public ResponsesAddCollection internalAddCollection;
	}
}
