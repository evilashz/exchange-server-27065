using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.EdgeSync
{
	// Token: 0x02000FD5 RID: 4053
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class EndPointNotRegisteredException : LocalizedException
	{
		// Token: 0x0600ADF9 RID: 44537 RVA: 0x002926D2 File Offset: 0x002908D2
		public EndPointNotRegisteredException() : base(Strings.EndPointNotRegisteredException)
		{
		}

		// Token: 0x0600ADFA RID: 44538 RVA: 0x002926DF File Offset: 0x002908DF
		public EndPointNotRegisteredException(Exception innerException) : base(Strings.EndPointNotRegisteredException, innerException)
		{
		}

		// Token: 0x0600ADFB RID: 44539 RVA: 0x002926ED File Offset: 0x002908ED
		protected EndPointNotRegisteredException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600ADFC RID: 44540 RVA: 0x002926F7 File Offset: 0x002908F7
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
