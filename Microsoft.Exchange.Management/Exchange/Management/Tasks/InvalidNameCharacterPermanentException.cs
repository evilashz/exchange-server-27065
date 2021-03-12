using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000EC1 RID: 3777
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidNameCharacterPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x0600A89C RID: 43164 RVA: 0x0028A43D File Offset: 0x0028863D
		public InvalidNameCharacterPermanentException(string name, string character) : base(Strings.ErrorInvalidNameCharacter(name, character))
		{
			this.name = name;
			this.character = character;
		}

		// Token: 0x0600A89D RID: 43165 RVA: 0x0028A45A File Offset: 0x0028865A
		public InvalidNameCharacterPermanentException(string name, string character, Exception innerException) : base(Strings.ErrorInvalidNameCharacter(name, character), innerException)
		{
			this.name = name;
			this.character = character;
		}

		// Token: 0x0600A89E RID: 43166 RVA: 0x0028A478 File Offset: 0x00288678
		protected InvalidNameCharacterPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.name = (string)info.GetValue("name", typeof(string));
			this.character = (string)info.GetValue("character", typeof(string));
		}

		// Token: 0x0600A89F RID: 43167 RVA: 0x0028A4CD File Offset: 0x002886CD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("name", this.name);
			info.AddValue("character", this.character);
		}

		// Token: 0x170036B5 RID: 14005
		// (get) Token: 0x0600A8A0 RID: 43168 RVA: 0x0028A4F9 File Offset: 0x002886F9
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170036B6 RID: 14006
		// (get) Token: 0x0600A8A1 RID: 43169 RVA: 0x0028A501 File Offset: 0x00288701
		public string Character
		{
			get
			{
				return this.character;
			}
		}

		// Token: 0x0400601B RID: 24603
		private readonly string name;

		// Token: 0x0400601C RID: 24604
		private readonly string character;
	}
}
