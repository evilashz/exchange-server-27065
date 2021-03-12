using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.UM.UMCommon.Exceptions;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020001DB RID: 475
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TooManyOustandingMwiRequestsException : MwiDeliveryException
	{
		// Token: 0x06000F68 RID: 3944 RVA: 0x00036666 File Offset: 0x00034866
		public TooManyOustandingMwiRequestsException(string userName) : base(Strings.descTooManyOutstandingMwiRequestsError(userName))
		{
			this.userName = userName;
		}

		// Token: 0x06000F69 RID: 3945 RVA: 0x0003667B File Offset: 0x0003487B
		public TooManyOustandingMwiRequestsException(string userName, Exception innerException) : base(Strings.descTooManyOutstandingMwiRequestsError(userName), innerException)
		{
			this.userName = userName;
		}

		// Token: 0x06000F6A RID: 3946 RVA: 0x00036691 File Offset: 0x00034891
		protected TooManyOustandingMwiRequestsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.userName = (string)info.GetValue("userName", typeof(string));
		}

		// Token: 0x06000F6B RID: 3947 RVA: 0x000366BB File Offset: 0x000348BB
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("userName", this.userName);
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06000F6C RID: 3948 RVA: 0x000366D6 File Offset: 0x000348D6
		public string UserName
		{
			get
			{
				return this.userName;
			}
		}

		// Token: 0x040007AC RID: 1964
		private readonly string userName;
	}
}
