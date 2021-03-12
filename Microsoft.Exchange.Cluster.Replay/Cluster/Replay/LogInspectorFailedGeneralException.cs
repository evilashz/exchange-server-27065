using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020004D5 RID: 1237
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class LogInspectorFailedGeneralException : LogInspectorFailedException
	{
		// Token: 0x06002E0D RID: 11789 RVA: 0x000C2A91 File Offset: 0x000C0C91
		public LogInspectorFailedGeneralException(string fileName, string specificError) : base(ReplayStrings.LogInspectorFailedGeneral(fileName, specificError))
		{
			this.fileName = fileName;
			this.specificError = specificError;
		}

		// Token: 0x06002E0E RID: 11790 RVA: 0x000C2AB3 File Offset: 0x000C0CB3
		public LogInspectorFailedGeneralException(string fileName, string specificError, Exception innerException) : base(ReplayStrings.LogInspectorFailedGeneral(fileName, specificError), innerException)
		{
			this.fileName = fileName;
			this.specificError = specificError;
		}

		// Token: 0x06002E0F RID: 11791 RVA: 0x000C2AD8 File Offset: 0x000C0CD8
		protected LogInspectorFailedGeneralException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.fileName = (string)info.GetValue("fileName", typeof(string));
			this.specificError = (string)info.GetValue("specificError", typeof(string));
		}

		// Token: 0x06002E10 RID: 11792 RVA: 0x000C2B2D File Offset: 0x000C0D2D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("fileName", this.fileName);
			info.AddValue("specificError", this.specificError);
		}

		// Token: 0x17000BC3 RID: 3011
		// (get) Token: 0x06002E11 RID: 11793 RVA: 0x000C2B59 File Offset: 0x000C0D59
		public string FileName
		{
			get
			{
				return this.fileName;
			}
		}

		// Token: 0x17000BC4 RID: 3012
		// (get) Token: 0x06002E12 RID: 11794 RVA: 0x000C2B61 File Offset: 0x000C0D61
		public string SpecificError
		{
			get
			{
				return this.specificError;
			}
		}

		// Token: 0x04001560 RID: 5472
		private readonly string fileName;

		// Token: 0x04001561 RID: 5473
		private readonly string specificError;
	}
}
