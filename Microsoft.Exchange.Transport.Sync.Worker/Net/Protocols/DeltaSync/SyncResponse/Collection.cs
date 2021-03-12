using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SyncResponse
{
	// Token: 0x020001BB RID: 443
	[XmlType(TypeName = "Collection", Namespace = "AirSync:")]
	[Serializable]
	public class Collection
	{
		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06000C66 RID: 3174 RVA: 0x0001EA3D File Offset: 0x0001CC3D
		// (set) Token: 0x06000C67 RID: 3175 RVA: 0x0001EA45 File Offset: 0x0001CC45
		[XmlIgnore]
		public string Class
		{
			get
			{
				return this.internalClass;
			}
			set
			{
				this.internalClass = value;
			}
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06000C68 RID: 3176 RVA: 0x0001EA4E File Offset: 0x0001CC4E
		// (set) Token: 0x06000C69 RID: 3177 RVA: 0x0001EA56 File Offset: 0x0001CC56
		[XmlIgnore]
		public string SyncKey
		{
			get
			{
				return this.internalSyncKey;
			}
			set
			{
				this.internalSyncKey = value;
			}
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06000C6A RID: 3178 RVA: 0x0001EA5F File Offset: 0x0001CC5F
		// (set) Token: 0x06000C6B RID: 3179 RVA: 0x0001EA67 File Offset: 0x0001CC67
		[XmlIgnore]
		public int Status
		{
			get
			{
				return this.internalStatus;
			}
			set
			{
				this.internalStatus = value;
				this.internalStatusSpecified = true;
			}
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06000C6C RID: 3180 RVA: 0x0001EA77 File Offset: 0x0001CC77
		// (set) Token: 0x06000C6D RID: 3181 RVA: 0x0001EA92 File Offset: 0x0001CC92
		[XmlIgnore]
		public Commands Commands
		{
			get
			{
				if (this.internalCommands == null)
				{
					this.internalCommands = new Commands();
				}
				return this.internalCommands;
			}
			set
			{
				this.internalCommands = value;
			}
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06000C6E RID: 3182 RVA: 0x0001EA9B File Offset: 0x0001CC9B
		// (set) Token: 0x06000C6F RID: 3183 RVA: 0x0001EAB6 File Offset: 0x0001CCB6
		[XmlIgnore]
		public Responses Responses
		{
			get
			{
				if (this.internalResponses == null)
				{
					this.internalResponses = new Responses();
				}
				return this.internalResponses;
			}
			set
			{
				this.internalResponses = value;
			}
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06000C70 RID: 3184 RVA: 0x0001EABF File Offset: 0x0001CCBF
		// (set) Token: 0x06000C71 RID: 3185 RVA: 0x0001EADA File Offset: 0x0001CCDA
		[XmlIgnore]
		public MoreAvailable MoreAvailable
		{
			get
			{
				if (this.internalMoreAvailable == null)
				{
					this.internalMoreAvailable = new MoreAvailable();
				}
				return this.internalMoreAvailable;
			}
			set
			{
				this.internalMoreAvailable = value;
			}
		}

		// Token: 0x040006F0 RID: 1776
		[XmlElement(ElementName = "Class", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = "AirSync:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string internalClass;

		// Token: 0x040006F1 RID: 1777
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(ElementName = "SyncKey", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "string", Namespace = "AirSync:")]
		public string internalSyncKey;

		// Token: 0x040006F2 RID: 1778
		[XmlElement(ElementName = "Status", IsNullable = false, Form = XmlSchemaForm.Qualified, DataType = "int", Namespace = "AirSync:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public int internalStatus;

		// Token: 0x040006F3 RID: 1779
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlIgnore]
		public bool internalStatusSpecified;

		// Token: 0x040006F4 RID: 1780
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(Commands), ElementName = "Commands", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "AirSync:")]
		public Commands internalCommands;

		// Token: 0x040006F5 RID: 1781
		[XmlElement(Type = typeof(Responses), ElementName = "Responses", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "AirSync:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Responses internalResponses;

		// Token: 0x040006F6 RID: 1782
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(MoreAvailable), ElementName = "MoreAvailable", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "AirSync:")]
		public MoreAvailable internalMoreAvailable;
	}
}
