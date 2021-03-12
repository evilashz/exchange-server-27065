using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200021B RID: 539
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class PersonalContactsSpeechGrammarTimeoutException : SpeechGrammarException
	{
		// Token: 0x0600113E RID: 4414 RVA: 0x00039D04 File Offset: 0x00037F04
		public PersonalContactsSpeechGrammarTimeoutException(string user) : base(Strings.PersonalContactsSpeechGrammarTimeoutException(user))
		{
			this.user = user;
		}

		// Token: 0x0600113F RID: 4415 RVA: 0x00039D19 File Offset: 0x00037F19
		public PersonalContactsSpeechGrammarTimeoutException(string user, Exception innerException) : base(Strings.PersonalContactsSpeechGrammarTimeoutException(user), innerException)
		{
			this.user = user;
		}

		// Token: 0x06001140 RID: 4416 RVA: 0x00039D2F File Offset: 0x00037F2F
		protected PersonalContactsSpeechGrammarTimeoutException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.user = (string)info.GetValue("user", typeof(string));
		}

		// Token: 0x06001141 RID: 4417 RVA: 0x00039D59 File Offset: 0x00037F59
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("user", this.user);
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06001142 RID: 4418 RVA: 0x00039D74 File Offset: 0x00037F74
		public string User
		{
			get
			{
				return this.user;
			}
		}

		// Token: 0x04000890 RID: 2192
		private readonly string user;
	}
}
