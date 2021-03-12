using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000DD6 RID: 3542
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnSupportedUMLanguageException : LocalizedException
	{
		// Token: 0x0600A41C RID: 42012 RVA: 0x00283B2D File Offset: 0x00281D2D
		public UnSupportedUMLanguageException(string language) : base(Strings.UnSupportedUMLanguageException(language))
		{
			this.language = language;
		}

		// Token: 0x0600A41D RID: 42013 RVA: 0x00283B42 File Offset: 0x00281D42
		public UnSupportedUMLanguageException(string language, Exception innerException) : base(Strings.UnSupportedUMLanguageException(language), innerException)
		{
			this.language = language;
		}

		// Token: 0x0600A41E RID: 42014 RVA: 0x00283B58 File Offset: 0x00281D58
		protected UnSupportedUMLanguageException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.language = (string)info.GetValue("language", typeof(string));
		}

		// Token: 0x0600A41F RID: 42015 RVA: 0x00283B82 File Offset: 0x00281D82
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("language", this.language);
		}

		// Token: 0x170035E1 RID: 13793
		// (get) Token: 0x0600A420 RID: 42016 RVA: 0x00283B9D File Offset: 0x00281D9D
		public string Language
		{
			get
			{
				return this.language;
			}
		}

		// Token: 0x04005F47 RID: 24391
		private readonly string language;
	}
}
