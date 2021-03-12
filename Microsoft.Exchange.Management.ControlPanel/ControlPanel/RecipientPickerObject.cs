using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.Management;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200034F RID: 847
	[DataContract]
	public class RecipientPickerObject : BaseRow
	{
		// Token: 0x06002F83 RID: 12163 RVA: 0x0009119F File Offset: 0x0008F39F
		public RecipientPickerObject(ReducedRecipient recipient) : base(recipient.ToIdentity(), recipient)
		{
			this.Recipient = recipient;
		}

		// Token: 0x17001EFB RID: 7931
		// (get) Token: 0x06002F84 RID: 12164 RVA: 0x000911B5 File Offset: 0x0008F3B5
		// (set) Token: 0x06002F85 RID: 12165 RVA: 0x000911BD File Offset: 0x0008F3BD
		public ReducedRecipient Recipient { get; private set; }

		// Token: 0x17001EFC RID: 7932
		// (get) Token: 0x06002F86 RID: 12166 RVA: 0x000911C6 File Offset: 0x0008F3C6
		// (set) Token: 0x06002F87 RID: 12167 RVA: 0x000911F1 File Offset: 0x0008F3F1
		[DataMember]
		public virtual string DisplayName
		{
			get
			{
				if (!string.IsNullOrEmpty(this.Recipient.DisplayName))
				{
					return this.Recipient.DisplayName;
				}
				return this.Recipient.Name;
			}
			protected set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001EFD RID: 7933
		// (get) Token: 0x06002F88 RID: 12168 RVA: 0x000911F8 File Offset: 0x0008F3F8
		// (set) Token: 0x06002F89 RID: 12169 RVA: 0x00091205 File Offset: 0x0008F405
		[DataMember]
		public virtual string Alias
		{
			get
			{
				return this.Recipient.Alias;
			}
			protected set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001EFE RID: 7934
		// (get) Token: 0x06002F8A RID: 12170 RVA: 0x0009120C File Offset: 0x0008F40C
		// (set) Token: 0x06002F8B RID: 12171 RVA: 0x00091223 File Offset: 0x0008F423
		[DataMember]
		public virtual string RecipientType
		{
			get
			{
				return this.Recipient.RecipientType.ToString();
			}
			protected set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001EFF RID: 7935
		// (get) Token: 0x06002F8C RID: 12172 RVA: 0x0009122C File Offset: 0x0008F42C
		// (set) Token: 0x06002F8D RID: 12173 RVA: 0x00091252 File Offset: 0x0008F452
		[DataMember]
		public virtual string PrimarySmtpAddress
		{
			get
			{
				return this.Recipient.PrimarySmtpAddress.ToString();
			}
			protected set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001F00 RID: 7936
		// (get) Token: 0x06002F8E RID: 12174 RVA: 0x00091259 File Offset: 0x0008F459
		// (set) Token: 0x06002F8F RID: 12175 RVA: 0x00091266 File Offset: 0x0008F466
		[DataMember]
		public virtual string DistinguishedName
		{
			get
			{
				return this.Recipient.DistinguishedName;
			}
			protected set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001F01 RID: 7937
		// (get) Token: 0x06002F90 RID: 12176 RVA: 0x0009126D File Offset: 0x0008F46D
		// (set) Token: 0x06002F91 RID: 12177 RVA: 0x0009127F File Offset: 0x0008F47F
		[DataMember]
		public string SpriteId
		{
			get
			{
				return Icons.FromEnum(this.Recipient.RecipientTypeDetails);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001F02 RID: 7938
		// (get) Token: 0x06002F92 RID: 12178 RVA: 0x00091286 File Offset: 0x0008F486
		// (set) Token: 0x06002F93 RID: 12179 RVA: 0x00091298 File Offset: 0x0008F498
		[DataMember]
		public string IconAltText
		{
			get
			{
				return Icons.GenerateIconAltText(this.Recipient.RecipientTypeDetails);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}
	}
}
