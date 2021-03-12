using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x02000D60 RID: 3424
	[Serializable]
	public class UMMailboxPin : IConfigurable
	{
		// Token: 0x06008356 RID: 33622 RVA: 0x00218944 File Offset: 0x00216B44
		public UMMailboxPin(ADUser user, bool expired, bool lockedOut, bool firstTimeUser, bool needSuppressingPiiData)
		{
			this.userObjectId = user.Identity;
			this.userId = user.PrimarySmtpAddress.ToString();
			this.pinExpired = expired;
			this.lockedOut = lockedOut;
			this.isFirstTimeUser = firstTimeUser;
			if (needSuppressingPiiData)
			{
				ADObjectId id = user.Id;
				string text;
				string text2;
				this.userObjectId = SuppressingPiiData.Redact(id, out text, out text2);
				this.userId = SuppressingPiiData.RedactSmtpAddress(this.userId);
			}
		}

		// Token: 0x06008357 RID: 33623 RVA: 0x002189BF File Offset: 0x00216BBF
		private UMMailboxPin()
		{
		}

		// Token: 0x170028E1 RID: 10465
		// (get) Token: 0x06008358 RID: 33624 RVA: 0x002189C7 File Offset: 0x00216BC7
		public string UserID
		{
			get
			{
				return this.userId;
			}
		}

		// Token: 0x170028E2 RID: 10466
		// (get) Token: 0x06008359 RID: 33625 RVA: 0x002189CF File Offset: 0x00216BCF
		// (set) Token: 0x0600835A RID: 33626 RVA: 0x002189D7 File Offset: 0x00216BD7
		public bool PinExpired
		{
			get
			{
				return this.pinExpired;
			}
			internal set
			{
				this.pinExpired = value;
			}
		}

		// Token: 0x170028E3 RID: 10467
		// (get) Token: 0x0600835B RID: 33627 RVA: 0x002189E0 File Offset: 0x00216BE0
		// (set) Token: 0x0600835C RID: 33628 RVA: 0x002189E8 File Offset: 0x00216BE8
		public bool FirstTimeUser
		{
			get
			{
				return this.isFirstTimeUser;
			}
			internal set
			{
				this.isFirstTimeUser = value;
			}
		}

		// Token: 0x170028E4 RID: 10468
		// (get) Token: 0x0600835D RID: 33629 RVA: 0x002189F1 File Offset: 0x00216BF1
		// (set) Token: 0x0600835E RID: 33630 RVA: 0x002189F9 File Offset: 0x00216BF9
		public bool LockedOut
		{
			get
			{
				return this.lockedOut;
			}
			internal set
			{
				this.lockedOut = value;
			}
		}

		// Token: 0x170028E5 RID: 10469
		// (get) Token: 0x0600835F RID: 33631 RVA: 0x00218A02 File Offset: 0x00216C02
		public ObjectState ObjectState
		{
			get
			{
				return ObjectState.Unchanged;
			}
		}

		// Token: 0x170028E6 RID: 10470
		// (get) Token: 0x06008360 RID: 33632 RVA: 0x00218A05 File Offset: 0x00216C05
		public bool IsValid
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170028E7 RID: 10471
		// (get) Token: 0x06008361 RID: 33633 RVA: 0x00218A08 File Offset: 0x00216C08
		ObjectId IConfigurable.Identity
		{
			get
			{
				return this.userObjectId;
			}
		}

		// Token: 0x06008362 RID: 33634 RVA: 0x00218A10 File Offset: 0x00216C10
		public void CopyChangesFrom(IConfigurable source)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06008363 RID: 33635 RVA: 0x00218A17 File Offset: 0x00216C17
		public void ResetChangeTracking()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06008364 RID: 33636 RVA: 0x00218A1E File Offset: 0x00216C1E
		public virtual ValidationError[] Validate()
		{
			return ValidationError.None;
		}

		// Token: 0x04003FC4 RID: 16324
		private readonly string userId;

		// Token: 0x04003FC5 RID: 16325
		private ObjectId userObjectId;

		// Token: 0x04003FC6 RID: 16326
		private bool pinExpired;

		// Token: 0x04003FC7 RID: 16327
		private bool lockedOut;

		// Token: 0x04003FC8 RID: 16328
		private bool isFirstTimeUser;
	}
}
