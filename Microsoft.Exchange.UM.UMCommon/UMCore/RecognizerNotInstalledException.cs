using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000214 RID: 532
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class RecognizerNotInstalledException : LocalizedException
	{
		// Token: 0x0600111C RID: 4380 RVA: 0x000399FA File Offset: 0x00037BFA
		public RecognizerNotInstalledException(string engineType, string language) : base(Strings.RecognizerNotInstalled(engineType, language))
		{
			this.engineType = engineType;
			this.language = language;
		}

		// Token: 0x0600111D RID: 4381 RVA: 0x00039A17 File Offset: 0x00037C17
		public RecognizerNotInstalledException(string engineType, string language, Exception innerException) : base(Strings.RecognizerNotInstalled(engineType, language), innerException)
		{
			this.engineType = engineType;
			this.language = language;
		}

		// Token: 0x0600111E RID: 4382 RVA: 0x00039A38 File Offset: 0x00037C38
		protected RecognizerNotInstalledException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.engineType = (string)info.GetValue("engineType", typeof(string));
			this.language = (string)info.GetValue("language", typeof(string));
		}

		// Token: 0x0600111F RID: 4383 RVA: 0x00039A8D File Offset: 0x00037C8D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("engineType", this.engineType);
			info.AddValue("language", this.language);
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06001120 RID: 4384 RVA: 0x00039AB9 File Offset: 0x00037CB9
		public string EngineType
		{
			get
			{
				return this.engineType;
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06001121 RID: 4385 RVA: 0x00039AC1 File Offset: 0x00037CC1
		public string Language
		{
			get
			{
				return this.language;
			}
		}

		// Token: 0x0400088A RID: 2186
		private readonly string engineType;

		// Token: 0x0400088B RID: 2187
		private readonly string language;
	}
}
