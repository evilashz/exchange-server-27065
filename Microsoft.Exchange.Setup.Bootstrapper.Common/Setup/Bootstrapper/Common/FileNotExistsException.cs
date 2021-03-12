using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Setup.Bootstrapper.Common
{
	// Token: 0x02000017 RID: 23
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class FileNotExistsException : LocalizedException
	{
		// Token: 0x060000CE RID: 206 RVA: 0x00005019 File Offset: 0x00003219
		public FileNotExistsException(string fullFilePath) : base(Strings.FileNotExistsError(fullFilePath))
		{
			this.fullFilePath = fullFilePath;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x0000502E File Offset: 0x0000322E
		public FileNotExistsException(string fullFilePath, Exception innerException) : base(Strings.FileNotExistsError(fullFilePath), innerException)
		{
			this.fullFilePath = fullFilePath;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00005044 File Offset: 0x00003244
		protected FileNotExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.fullFilePath = (string)info.GetValue("fullFilePath", typeof(string));
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x0000506E File Offset: 0x0000326E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("fullFilePath", this.fullFilePath);
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00005089 File Offset: 0x00003289
		public string FullFilePath
		{
			get
			{
				return this.fullFilePath;
			}
		}

		// Token: 0x040000BF RID: 191
		private readonly string fullFilePath;
	}
}
