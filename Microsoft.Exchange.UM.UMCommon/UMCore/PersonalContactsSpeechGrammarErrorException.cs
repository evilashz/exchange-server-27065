using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.UM.UMCore.Exceptions;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200021A RID: 538
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class PersonalContactsSpeechGrammarErrorException : SpeechGrammarException
	{
		// Token: 0x06001139 RID: 4409 RVA: 0x00039C8C File Offset: 0x00037E8C
		public PersonalContactsSpeechGrammarErrorException(string user) : base(Strings.PersonalContactsSpeechGrammarErrorException(user))
		{
			this.user = user;
		}

		// Token: 0x0600113A RID: 4410 RVA: 0x00039CA1 File Offset: 0x00037EA1
		public PersonalContactsSpeechGrammarErrorException(string user, Exception innerException) : base(Strings.PersonalContactsSpeechGrammarErrorException(user), innerException)
		{
			this.user = user;
		}

		// Token: 0x0600113B RID: 4411 RVA: 0x00039CB7 File Offset: 0x00037EB7
		protected PersonalContactsSpeechGrammarErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.user = (string)info.GetValue("user", typeof(string));
		}

		// Token: 0x0600113C RID: 4412 RVA: 0x00039CE1 File Offset: 0x00037EE1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("user", this.user);
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x0600113D RID: 4413 RVA: 0x00039CFC File Offset: 0x00037EFC
		public string User
		{
			get
			{
				return this.user;
			}
		}

		// Token: 0x0400088F RID: 2191
		private readonly string user;
	}
}
