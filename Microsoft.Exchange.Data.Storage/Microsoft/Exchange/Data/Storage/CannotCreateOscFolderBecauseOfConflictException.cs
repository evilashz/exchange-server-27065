using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200011E RID: 286
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class CannotCreateOscFolderBecauseOfConflictException : LocalizedException
	{
		// Token: 0x06001423 RID: 5155 RVA: 0x0006A49C File Offset: 0x0006869C
		public CannotCreateOscFolderBecauseOfConflictException(string provider) : base(ServerStrings.CannotCreateOscFolderBecauseOfConflict(provider))
		{
			this.provider = provider;
		}

		// Token: 0x06001424 RID: 5156 RVA: 0x0006A4B1 File Offset: 0x000686B1
		public CannotCreateOscFolderBecauseOfConflictException(string provider, Exception innerException) : base(ServerStrings.CannotCreateOscFolderBecauseOfConflict(provider), innerException)
		{
			this.provider = provider;
		}

		// Token: 0x06001425 RID: 5157 RVA: 0x0006A4C7 File Offset: 0x000686C7
		protected CannotCreateOscFolderBecauseOfConflictException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.provider = (string)info.GetValue("provider", typeof(string));
		}

		// Token: 0x06001426 RID: 5158 RVA: 0x0006A4F1 File Offset: 0x000686F1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("provider", this.provider);
		}

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x06001427 RID: 5159 RVA: 0x0006A50C File Offset: 0x0006870C
		public string Provider
		{
			get
			{
				return this.provider;
			}
		}

		// Token: 0x040009AB RID: 2475
		private readonly string provider;
	}
}
