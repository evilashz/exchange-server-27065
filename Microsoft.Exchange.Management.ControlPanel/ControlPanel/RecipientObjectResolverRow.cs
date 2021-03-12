using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000347 RID: 839
	[DataContract]
	public class RecipientObjectResolverRow : AdObjectResolverRow
	{
		// Token: 0x06002F53 RID: 12115 RVA: 0x000906D8 File Offset: 0x0008E8D8
		public RecipientObjectResolverRow(ADRawEntry aDRawEntry) : base(aDRawEntry)
		{
		}

		// Token: 0x17001EF0 RID: 7920
		// (get) Token: 0x06002F54 RID: 12116 RVA: 0x000906E4 File Offset: 0x0008E8E4
		public override string DisplayName
		{
			get
			{
				string text = (string)base.ADRawEntry[ADRecipientSchema.DisplayName];
				if (string.IsNullOrEmpty(text))
				{
					text = base.DisplayName;
				}
				return text;
			}
		}

		// Token: 0x17001EF1 RID: 7921
		// (get) Token: 0x06002F55 RID: 12117 RVA: 0x00090717 File Offset: 0x0008E917
		public RecipientTypeDetails RecipientTypeDetails
		{
			get
			{
				return (RecipientTypeDetails)base.ADRawEntry[ADRecipientSchema.RecipientTypeDetails];
			}
		}

		// Token: 0x17001EF2 RID: 7922
		// (get) Token: 0x06002F56 RID: 12118 RVA: 0x00090730 File Offset: 0x0008E930
		// (set) Token: 0x06002F57 RID: 12119 RVA: 0x00090760 File Offset: 0x0008E960
		[DataMember]
		public string PrimarySmtpAddress
		{
			get
			{
				return ((SmtpAddress)base.ADRawEntry[ADRecipientSchema.PrimarySmtpAddress]).ToString();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001EF3 RID: 7923
		// (get) Token: 0x06002F58 RID: 12120 RVA: 0x00090767 File Offset: 0x0008E967
		// (set) Token: 0x06002F59 RID: 12121 RVA: 0x00090783 File Offset: 0x0008E983
		[DataMember]
		public string SpriteId
		{
			get
			{
				return Icons.FromEnum((RecipientTypeDetails)base.ADRawEntry[ADRecipientSchema.RecipientTypeDetails]);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001EF4 RID: 7924
		// (get) Token: 0x06002F5A RID: 12122 RVA: 0x0009078A File Offset: 0x0008E98A
		// (set) Token: 0x06002F5B RID: 12123 RVA: 0x000907A6 File Offset: 0x0008E9A6
		[DataMember]
		public string IconAltText
		{
			get
			{
				return Icons.GenerateIconAltText((RecipientTypeDetails)base.ADRawEntry[ADRecipientSchema.RecipientTypeDetails]);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06002F5C RID: 12124 RVA: 0x000907B0 File Offset: 0x0008E9B0
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			RecipientObjectResolverRow recipientObjectResolverRow = obj as RecipientObjectResolverRow;
			return recipientObjectResolverRow != null && (string.Equals(this.DisplayName, recipientObjectResolverRow.DisplayName) && string.Equals(this.PrimarySmtpAddress, recipientObjectResolverRow.PrimarySmtpAddress) && string.Equals(this.IconAltText, recipientObjectResolverRow.IconAltText) && string.Equals(this.SpriteId, recipientObjectResolverRow.SpriteId)) && this.RecipientTypeDetails == recipientObjectResolverRow.RecipientTypeDetails;
		}

		// Token: 0x06002F5D RID: 12125 RVA: 0x0009082A File Offset: 0x0008EA2A
		public override int GetHashCode()
		{
			return this.PrimarySmtpAddress.GetHashCode();
		}

		// Token: 0x04002301 RID: 8961
		public new static PropertyDefinition[] Properties = new List<PropertyDefinition>(AdObjectResolverRow.Properties)
		{
			ADRecipientSchema.DisplayName,
			ADRecipientSchema.RecipientTypeDetails
		}.ToArray();
	}
}
