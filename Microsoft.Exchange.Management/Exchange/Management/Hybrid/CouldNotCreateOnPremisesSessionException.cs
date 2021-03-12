using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Hybrid
{
	// Token: 0x02001236 RID: 4662
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CouldNotCreateOnPremisesSessionException : LocalizedException
	{
		// Token: 0x0600BBDF RID: 48095 RVA: 0x002AB868 File Offset: 0x002A9A68
		public CouldNotCreateOnPremisesSessionException(Exception e) : base(HybridStrings.HybridCouldNotCreateOnPremisesSessionException(e))
		{
			this.e = e;
		}

		// Token: 0x0600BBE0 RID: 48096 RVA: 0x002AB87D File Offset: 0x002A9A7D
		public CouldNotCreateOnPremisesSessionException(Exception e, Exception innerException) : base(HybridStrings.HybridCouldNotCreateOnPremisesSessionException(e), innerException)
		{
			this.e = e;
		}

		// Token: 0x0600BBE1 RID: 48097 RVA: 0x002AB893 File Offset: 0x002A9A93
		protected CouldNotCreateOnPremisesSessionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.e = (Exception)info.GetValue("e", typeof(Exception));
		}

		// Token: 0x0600BBE2 RID: 48098 RVA: 0x002AB8BD File Offset: 0x002A9ABD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("e", this.e);
		}

		// Token: 0x17003B3E RID: 15166
		// (get) Token: 0x0600BBE3 RID: 48099 RVA: 0x002AB8D8 File Offset: 0x002A9AD8
		public Exception E
		{
			get
			{
				return this.e;
			}
		}

		// Token: 0x04006604 RID: 26116
		private readonly Exception e;
	}
}
