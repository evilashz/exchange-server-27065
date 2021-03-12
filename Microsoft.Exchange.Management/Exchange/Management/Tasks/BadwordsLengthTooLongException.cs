using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E7F RID: 3711
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class BadwordsLengthTooLongException : LocalizedException
	{
		// Token: 0x0600A74D RID: 42829 RVA: 0x002882CC File Offset: 0x002864CC
		public BadwordsLengthTooLongException(string prefix, int maxLength) : base(Strings.BadwordsLengthTooLongId(prefix, maxLength))
		{
			this.prefix = prefix;
			this.maxLength = maxLength;
		}

		// Token: 0x0600A74E RID: 42830 RVA: 0x002882E9 File Offset: 0x002864E9
		public BadwordsLengthTooLongException(string prefix, int maxLength, Exception innerException) : base(Strings.BadwordsLengthTooLongId(prefix, maxLength), innerException)
		{
			this.prefix = prefix;
			this.maxLength = maxLength;
		}

		// Token: 0x0600A74F RID: 42831 RVA: 0x00288308 File Offset: 0x00286508
		protected BadwordsLengthTooLongException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.prefix = (string)info.GetValue("prefix", typeof(string));
			this.maxLength = (int)info.GetValue("maxLength", typeof(int));
		}

		// Token: 0x0600A750 RID: 42832 RVA: 0x0028835D File Offset: 0x0028655D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("prefix", this.prefix);
			info.AddValue("maxLength", this.maxLength);
		}

		// Token: 0x1700366E RID: 13934
		// (get) Token: 0x0600A751 RID: 42833 RVA: 0x00288389 File Offset: 0x00286589
		public string Prefix
		{
			get
			{
				return this.prefix;
			}
		}

		// Token: 0x1700366F RID: 13935
		// (get) Token: 0x0600A752 RID: 42834 RVA: 0x00288391 File Offset: 0x00286591
		public int MaxLength
		{
			get
			{
				return this.maxLength;
			}
		}

		// Token: 0x04005FD4 RID: 24532
		private readonly string prefix;

		// Token: 0x04005FD5 RID: 24533
		private readonly int maxLength;
	}
}
