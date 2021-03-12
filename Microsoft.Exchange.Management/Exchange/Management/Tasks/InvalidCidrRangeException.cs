using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001024 RID: 4132
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidCidrRangeException : LocalizedException
	{
		// Token: 0x0600AF6E RID: 44910 RVA: 0x002945D7 File Offset: 0x002927D7
		public InvalidCidrRangeException(string ipRange, int minCidrLength) : base(Strings.InvalidCidrRangeId(ipRange, minCidrLength))
		{
			this.ipRange = ipRange;
			this.minCidrLength = minCidrLength;
		}

		// Token: 0x0600AF6F RID: 44911 RVA: 0x002945F4 File Offset: 0x002927F4
		public InvalidCidrRangeException(string ipRange, int minCidrLength, Exception innerException) : base(Strings.InvalidCidrRangeId(ipRange, minCidrLength), innerException)
		{
			this.ipRange = ipRange;
			this.minCidrLength = minCidrLength;
		}

		// Token: 0x0600AF70 RID: 44912 RVA: 0x00294614 File Offset: 0x00292814
		protected InvalidCidrRangeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.ipRange = (string)info.GetValue("ipRange", typeof(string));
			this.minCidrLength = (int)info.GetValue("minCidrLength", typeof(int));
		}

		// Token: 0x0600AF71 RID: 44913 RVA: 0x00294669 File Offset: 0x00292869
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ipRange", this.ipRange);
			info.AddValue("minCidrLength", this.minCidrLength);
		}

		// Token: 0x170037FB RID: 14331
		// (get) Token: 0x0600AF72 RID: 44914 RVA: 0x00294695 File Offset: 0x00292895
		public string IpRange
		{
			get
			{
				return this.ipRange;
			}
		}

		// Token: 0x170037FC RID: 14332
		// (get) Token: 0x0600AF73 RID: 44915 RVA: 0x0029469D File Offset: 0x0029289D
		public int MinCidrLength
		{
			get
			{
				return this.minCidrLength;
			}
		}

		// Token: 0x04006161 RID: 24929
		private readonly string ipRange;

		// Token: 0x04006162 RID: 24930
		private readonly int minCidrLength;
	}
}
