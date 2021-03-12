using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000EA RID: 234
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class FilterOnlyAttributesException : LocalizedException
	{
		// Token: 0x06000830 RID: 2096 RVA: 0x0001B26E File Offset: 0x0001946E
		public FilterOnlyAttributesException(string attributeName) : base(DataStrings.FilterOnlyAttributes(attributeName))
		{
			this.attributeName = attributeName;
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x0001B283 File Offset: 0x00019483
		public FilterOnlyAttributesException(string attributeName, Exception innerException) : base(DataStrings.FilterOnlyAttributes(attributeName), innerException)
		{
			this.attributeName = attributeName;
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x0001B299 File Offset: 0x00019499
		protected FilterOnlyAttributesException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.attributeName = (string)info.GetValue("attributeName", typeof(string));
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x0001B2C3 File Offset: 0x000194C3
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("attributeName", this.attributeName);
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000834 RID: 2100 RVA: 0x0001B2DE File Offset: 0x000194DE
		public string AttributeName
		{
			get
			{
				return this.attributeName;
			}
		}

		// Token: 0x0400058E RID: 1422
		private readonly string attributeName;
	}
}
