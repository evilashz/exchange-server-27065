using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Management.Transport
{
	// Token: 0x0200017E RID: 382
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class InvalidContentDateFromAndContentDateToPredicateException : InvalidComplianceRulePredicateException
	{
		// Token: 0x06000F6E RID: 3950 RVA: 0x00036427 File Offset: 0x00034627
		public InvalidContentDateFromAndContentDateToPredicateException() : base(Strings.InvalidContentDateFromAndContentDateToPredicate)
		{
		}

		// Token: 0x06000F6F RID: 3951 RVA: 0x00036434 File Offset: 0x00034634
		public InvalidContentDateFromAndContentDateToPredicateException(Exception innerException) : base(Strings.InvalidContentDateFromAndContentDateToPredicate, innerException)
		{
		}

		// Token: 0x06000F70 RID: 3952 RVA: 0x00036442 File Offset: 0x00034642
		protected InvalidContentDateFromAndContentDateToPredicateException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000F71 RID: 3953 RVA: 0x0003644C File Offset: 0x0003464C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
