using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;
using Microsoft.Exchange.Net.Protocols.DeltaSync.HMSync;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.SyncRequest
{
	// Token: 0x020001A9 RID: 425
	[XmlRoot(ElementName = "Sync", Namespace = "AirSync:", IsNullable = false)]
	[Serializable]
	public class Sync
	{
		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06000BED RID: 3053 RVA: 0x0001E42A File Offset: 0x0001C62A
		// (set) Token: 0x06000BEE RID: 3054 RVA: 0x0001E445 File Offset: 0x0001C645
		[XmlIgnore]
		public DeletesAsMoves DeletesAsMoves
		{
			get
			{
				if (this.internalDeletesAsMoves == null)
				{
					this.internalDeletesAsMoves = new DeletesAsMoves();
				}
				return this.internalDeletesAsMoves;
			}
			set
			{
				this.internalDeletesAsMoves = value;
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06000BEF RID: 3055 RVA: 0x0001E44E File Offset: 0x0001C64E
		// (set) Token: 0x06000BF0 RID: 3056 RVA: 0x0001E469 File Offset: 0x0001C669
		[XmlIgnore]
		public Options Options
		{
			get
			{
				if (this.internalOptions == null)
				{
					this.internalOptions = new Options();
				}
				return this.internalOptions;
			}
			set
			{
				this.internalOptions = value;
			}
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06000BF1 RID: 3057 RVA: 0x0001E472 File Offset: 0x0001C672
		// (set) Token: 0x06000BF2 RID: 3058 RVA: 0x0001E48D File Offset: 0x0001C68D
		[XmlIgnore]
		public Collections Collections
		{
			get
			{
				if (this.internalCollections == null)
				{
					this.internalCollections = new Collections();
				}
				return this.internalCollections;
			}
			set
			{
				this.internalCollections = value;
			}
		}

		// Token: 0x040006D5 RID: 1749
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(DeletesAsMoves), ElementName = "DeletesAsMoves", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSYNC:")]
		public DeletesAsMoves internalDeletesAsMoves;

		// Token: 0x040006D6 RID: 1750
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[XmlElement(Type = typeof(Options), ElementName = "Options", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "HMSYNC:")]
		public Options internalOptions;

		// Token: 0x040006D7 RID: 1751
		[XmlElement(Type = typeof(Collections), ElementName = "Collections", IsNullable = false, Form = XmlSchemaForm.Qualified, Namespace = "AirSync:")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Collections internalCollections;
	}
}
