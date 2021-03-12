using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Model.Request.AirSync
{
	// Token: 0x02000094 RID: 148
	[ClassAccessLevel(AccessLevel.Implementation)]
	[XmlType(Namespace = "AirSync", TypeName = "Collection")]
	public class Collection
	{
		// Token: 0x0600031C RID: 796 RVA: 0x00009B0D File Offset: 0x00007D0D
		public Collection()
		{
			this.Options = new List<Options>();
			this.Commands = new List<Command>();
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600031D RID: 797 RVA: 0x00009B2B File Offset: 0x00007D2B
		// (set) Token: 0x0600031E RID: 798 RVA: 0x00009B33 File Offset: 0x00007D33
		[XmlElement(ElementName = "SyncKey")]
		public string SyncKey { get; set; }

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x0600031F RID: 799 RVA: 0x00009B3C File Offset: 0x00007D3C
		// (set) Token: 0x06000320 RID: 800 RVA: 0x00009B44 File Offset: 0x00007D44
		[XmlElement(ElementName = "NotifyGUID")]
		public string NotifyGuid { get; set; }

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000321 RID: 801 RVA: 0x00009B4D File Offset: 0x00007D4D
		// (set) Token: 0x06000322 RID: 802 RVA: 0x00009B55 File Offset: 0x00007D55
		[XmlElement(ElementName = "CollectionId")]
		public string CollectionId { get; set; }

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000323 RID: 803 RVA: 0x00009B5E File Offset: 0x00007D5E
		// (set) Token: 0x06000324 RID: 804 RVA: 0x00009B66 File Offset: 0x00007D66
		[XmlIgnore]
		public bool? DeletesAsMoves { get; set; }

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000325 RID: 805 RVA: 0x00009B70 File Offset: 0x00007D70
		// (set) Token: 0x06000326 RID: 806 RVA: 0x00009BA4 File Offset: 0x00007DA4
		[XmlElement(ElementName = "DeletesAsMoves")]
		public string SerializableDeletesAsMoves
		{
			get
			{
				if (!(this.DeletesAsMoves == true))
				{
					return "0";
				}
				return "1";
			}
			set
			{
				this.DeletesAsMoves = new bool?(XmlConvert.ToBoolean(value));
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000327 RID: 807 RVA: 0x00009BB7 File Offset: 0x00007DB7
		// (set) Token: 0x06000328 RID: 808 RVA: 0x00009BBF File Offset: 0x00007DBF
		[XmlIgnore]
		public bool? GetChanges { get; set; }

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000329 RID: 809 RVA: 0x00009BC8 File Offset: 0x00007DC8
		// (set) Token: 0x0600032A RID: 810 RVA: 0x00009BFC File Offset: 0x00007DFC
		[XmlElement(ElementName = "GetChanges")]
		public string SerializableGetChanges
		{
			get
			{
				if (!(this.GetChanges == true))
				{
					return "0";
				}
				return "1";
			}
			set
			{
				this.GetChanges = new bool?(XmlConvert.ToBoolean(value));
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x0600032B RID: 811 RVA: 0x00009C0F File Offset: 0x00007E0F
		// (set) Token: 0x0600032C RID: 812 RVA: 0x00009C17 File Offset: 0x00007E17
		[XmlElement(ElementName = "WindowSize")]
		public int? WindowSize { get; set; }

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x0600032D RID: 813 RVA: 0x00009C20 File Offset: 0x00007E20
		// (set) Token: 0x0600032E RID: 814 RVA: 0x00009C28 File Offset: 0x00007E28
		[XmlElement(ElementName = "ConversationMode")]
		public object ConversationMode { get; set; }

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x0600032F RID: 815 RVA: 0x00009C31 File Offset: 0x00007E31
		// (set) Token: 0x06000330 RID: 816 RVA: 0x00009C39 File Offset: 0x00007E39
		[XmlElement(ElementName = "Options")]
		public List<Options> Options { get; set; }

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000331 RID: 817 RVA: 0x00009C42 File Offset: 0x00007E42
		// (set) Token: 0x06000332 RID: 818 RVA: 0x00009C4A File Offset: 0x00007E4A
		[XmlArrayItem("Fetch", typeof(FetchCommand))]
		[XmlArrayItem("Delete", typeof(DeleteCommand))]
		[XmlArrayItem("Add", typeof(AddCommand))]
		[XmlArrayItem("Change", typeof(ChangeCommand))]
		public List<Command> Commands { get; set; }

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000333 RID: 819 RVA: 0x00009C53 File Offset: 0x00007E53
		[XmlIgnore]
		public bool CommandsSpecified
		{
			get
			{
				return this.Commands.Count > 0;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000334 RID: 820 RVA: 0x00009C64 File Offset: 0x00007E64
		[XmlIgnore]
		public bool SerializableDeletesAsMovesSpecified
		{
			get
			{
				return this.DeletesAsMoves != null;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000335 RID: 821 RVA: 0x00009C80 File Offset: 0x00007E80
		[XmlIgnore]
		public bool SerializableGetChangesSpecified
		{
			get
			{
				return this.GetChanges != null;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000336 RID: 822 RVA: 0x00009C9C File Offset: 0x00007E9C
		[XmlIgnore]
		public bool WindowSizeSpecified
		{
			get
			{
				return this.WindowSize != null;
			}
		}
	}
}
