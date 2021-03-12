using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011FE RID: 4606
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TcpAndTlsPortsCannotBeSameException : LocalizedException
	{
		// Token: 0x0600B9CE RID: 47566 RVA: 0x002A670D File Offset: 0x002A490D
		public TcpAndTlsPortsCannotBeSameException() : base(Strings.TcpAndTlsPortsCannotBeSame)
		{
		}

		// Token: 0x0600B9CF RID: 47567 RVA: 0x002A671A File Offset: 0x002A491A
		public TcpAndTlsPortsCannotBeSameException(Exception innerException) : base(Strings.TcpAndTlsPortsCannotBeSame, innerException)
		{
		}

		// Token: 0x0600B9D0 RID: 47568 RVA: 0x002A6728 File Offset: 0x002A4928
		protected TcpAndTlsPortsCannotBeSameException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B9D1 RID: 47569 RVA: 0x002A6732 File Offset: 0x002A4932
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
