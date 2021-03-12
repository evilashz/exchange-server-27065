using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Setup.Bootstrapper.Common
{
	// Token: 0x0200000E RID: 14
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class StartSetupFileNotFoundException : LocalizedException
	{
		// Token: 0x060000A6 RID: 166 RVA: 0x00004D49 File Offset: 0x00002F49
		public StartSetupFileNotFoundException(string fileName) : base(Strings.StartSetupFileNotFound(fileName))
		{
			this.fileName = fileName;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00004D5E File Offset: 0x00002F5E
		public StartSetupFileNotFoundException(string fileName, Exception innerException) : base(Strings.StartSetupFileNotFound(fileName), innerException)
		{
			this.fileName = fileName;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00004D74 File Offset: 0x00002F74
		protected StartSetupFileNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.fileName = (string)info.GetValue("fileName", typeof(string));
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00004D9E File Offset: 0x00002F9E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("fileName", this.fileName);
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000AA RID: 170 RVA: 0x00004DB9 File Offset: 0x00002FB9
		public string FileName
		{
			get
			{
				return this.fileName;
			}
		}

		// Token: 0x040000BB RID: 187
		private readonly string fileName;
	}
}
