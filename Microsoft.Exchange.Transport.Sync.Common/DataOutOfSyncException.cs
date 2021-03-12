using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x02000021 RID: 33
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class DataOutOfSyncException : LocalizedException
	{
		// Token: 0x0600014E RID: 334 RVA: 0x000053F5 File Offset: 0x000035F5
		public DataOutOfSyncException(int statusCode) : base(Strings.DataOutOfSyncException(statusCode))
		{
			this.statusCode = statusCode;
		}

		// Token: 0x0600014F RID: 335 RVA: 0x0000540A File Offset: 0x0000360A
		public DataOutOfSyncException(int statusCode, Exception innerException) : base(Strings.DataOutOfSyncException(statusCode), innerException)
		{
			this.statusCode = statusCode;
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00005420 File Offset: 0x00003620
		protected DataOutOfSyncException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.statusCode = (int)info.GetValue("statusCode", typeof(int));
		}

		// Token: 0x06000151 RID: 337 RVA: 0x0000544A File Offset: 0x0000364A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("statusCode", this.statusCode);
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000152 RID: 338 RVA: 0x00005465 File Offset: 0x00003665
		public int StatusCode
		{
			get
			{
				return this.statusCode;
			}
		}

		// Token: 0x040000E2 RID: 226
		private readonly int statusCode;
	}
}
