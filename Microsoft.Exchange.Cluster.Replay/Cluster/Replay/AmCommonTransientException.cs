using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200045A RID: 1114
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class AmCommonTransientException : AmServerTransientException
	{
		// Token: 0x06002B59 RID: 11097 RVA: 0x000BD484 File Offset: 0x000BB684
		public AmCommonTransientException(string error) : base(ReplayStrings.AmCommonTransientException(error))
		{
			this.error = error;
		}

		// Token: 0x06002B5A RID: 11098 RVA: 0x000BD49E File Offset: 0x000BB69E
		public AmCommonTransientException(string error, Exception innerException) : base(ReplayStrings.AmCommonTransientException(error), innerException)
		{
			this.error = error;
		}

		// Token: 0x06002B5B RID: 11099 RVA: 0x000BD4B9 File Offset: 0x000BB6B9
		protected AmCommonTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x06002B5C RID: 11100 RVA: 0x000BD4E3 File Offset: 0x000BB6E3
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("error", this.error);
		}

		// Token: 0x17000AFB RID: 2811
		// (get) Token: 0x06002B5D RID: 11101 RVA: 0x000BD4FE File Offset: 0x000BB6FE
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x04001498 RID: 5272
		private readonly string error;
	}
}
