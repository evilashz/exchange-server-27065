using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.MailboxTransport.ContentAggregation.Schema;
using Microsoft.Exchange.Net.Facebook;
using Microsoft.Exchange.Transport.Sync.Common;

namespace Microsoft.Exchange.Transport.Sync.Worker.Framework.Provider.Facebook
{
	// Token: 0x020001C9 RID: 457
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class FacebookContact : DisposeTrackableBase, ISyncContact, ISyncObject, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000D03 RID: 3331 RVA: 0x0001F00C File Offset: 0x0001D20C
		internal FacebookContact(FacebookUser user, ExDateTime peopleConnectionCreationTime)
		{
			SyncUtilities.ThrowIfArgumentNull("user", user);
			SyncUtilities.ThrowIfArgumentNullOrEmpty("user.Id", user.Id);
			this.user = user;
			this.InitializeBirthdate();
			this.peopleConnectionCreationTime = new ExDateTime?(peopleConnectionCreationTime);
			this.InitializeHobbies();
			this.InitializeCompanyNameAndJobTitle();
			this.InitializeSchools();
			this.InitializeOscContactSources();
			this.InitializePhotoUrl();
			this.SuppressDisposeTracker();
		}

		// Token: 0x06000D04 RID: 3332 RVA: 0x0001F077 File Offset: 0x0001D277
		private static bool DateTimeTryParseExact(string dateTime, out ExDateTime result)
		{
			return ExDateTime.TryParseExact(dateTime, "MM/dd/yyyy", null, DateTimeStyles.None, out result);
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x06000D05 RID: 3333 RVA: 0x0001F087 File Offset: 0x0001D287
		public SchemaType Type
		{
			get
			{
				base.CheckDisposed();
				return SchemaType.Contact;
			}
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x06000D06 RID: 3334 RVA: 0x0001F090 File Offset: 0x0001D290
		public ExDateTime? LastModifiedTime
		{
			get
			{
				base.CheckDisposed();
				ExDateTime value;
				if (ExDateTime.TryParse(this.user.UpdatedTime, out value))
				{
					return new ExDateTime?(value);
				}
				return null;
			}
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x06000D07 RID: 3335 RVA: 0x0001F0C7 File Offset: 0x0001D2C7
		public ExDateTime? BirthDate
		{
			get
			{
				base.CheckDisposed();
				return this.birthdate;
			}
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x06000D08 RID: 3336 RVA: 0x0001F0D5 File Offset: 0x0001D2D5
		public ExDateTime? BirthDateLocal
		{
			get
			{
				base.CheckDisposed();
				return this.birthdate;
			}
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x06000D09 RID: 3337 RVA: 0x0001F0E3 File Offset: 0x0001D2E3
		public string BusinessAddressCity
		{
			get
			{
				base.CheckDisposed();
				return null;
			}
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x06000D0A RID: 3338 RVA: 0x0001F0EC File Offset: 0x0001D2EC
		public string BusinessAddressCountry
		{
			get
			{
				base.CheckDisposed();
				return null;
			}
		}

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x06000D0B RID: 3339 RVA: 0x0001F0F5 File Offset: 0x0001D2F5
		public string BusinessAddressPostalCode
		{
			get
			{
				base.CheckDisposed();
				return null;
			}
		}

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x06000D0C RID: 3340 RVA: 0x0001F0FE File Offset: 0x0001D2FE
		public string BusinessAddressState
		{
			get
			{
				base.CheckDisposed();
				return null;
			}
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x06000D0D RID: 3341 RVA: 0x0001F107 File Offset: 0x0001D307
		public string BusinessAddressStreet
		{
			get
			{
				base.CheckDisposed();
				return null;
			}
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x06000D0E RID: 3342 RVA: 0x0001F110 File Offset: 0x0001D310
		public string BusinessFaxNumber
		{
			get
			{
				base.CheckDisposed();
				return null;
			}
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x06000D0F RID: 3343 RVA: 0x0001F119 File Offset: 0x0001D319
		public string BusinessTelephoneNumber
		{
			get
			{
				base.CheckDisposed();
				return null;
			}
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x06000D10 RID: 3344 RVA: 0x0001F122 File Offset: 0x0001D322
		public string CompanyName
		{
			get
			{
				base.CheckDisposed();
				return this.companyName;
			}
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x06000D11 RID: 3345 RVA: 0x0001F130 File Offset: 0x0001D330
		public string DisplayName
		{
			get
			{
				base.CheckDisposed();
				return null;
			}
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x06000D12 RID: 3346 RVA: 0x0001F139 File Offset: 0x0001D339
		public string Email1Address
		{
			get
			{
				base.CheckDisposed();
				return null;
			}
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x06000D13 RID: 3347 RVA: 0x0001F142 File Offset: 0x0001D342
		public string Email2Address
		{
			get
			{
				base.CheckDisposed();
				return null;
			}
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x06000D14 RID: 3348 RVA: 0x0001F14B File Offset: 0x0001D34B
		public string Email3Address
		{
			get
			{
				base.CheckDisposed();
				return null;
			}
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x06000D15 RID: 3349 RVA: 0x0001F154 File Offset: 0x0001D354
		public string FileAs
		{
			get
			{
				base.CheckDisposed();
				return null;
			}
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x06000D16 RID: 3350 RVA: 0x0001F15D File Offset: 0x0001D35D
		public string FirstName
		{
			get
			{
				base.CheckDisposed();
				return this.user.FirstName;
			}
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x06000D17 RID: 3351 RVA: 0x0001F170 File Offset: 0x0001D370
		public string Hobbies
		{
			get
			{
				base.CheckDisposed();
				return this.hobbies;
			}
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x06000D18 RID: 3352 RVA: 0x0001F17E File Offset: 0x0001D37E
		public string HomeAddressCity
		{
			get
			{
				base.CheckDisposed();
				if (this.user.Location == null)
				{
					return null;
				}
				return this.user.Location.Name;
			}
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x06000D19 RID: 3353 RVA: 0x0001F1A5 File Offset: 0x0001D3A5
		public string HomeAddressCountry
		{
			get
			{
				base.CheckDisposed();
				return null;
			}
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x06000D1A RID: 3354 RVA: 0x0001F1AE File Offset: 0x0001D3AE
		public string HomeAddressPostalCode
		{
			get
			{
				base.CheckDisposed();
				return null;
			}
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x06000D1B RID: 3355 RVA: 0x0001F1B7 File Offset: 0x0001D3B7
		public string HomeAddressState
		{
			get
			{
				base.CheckDisposed();
				return null;
			}
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x06000D1C RID: 3356 RVA: 0x0001F1C0 File Offset: 0x0001D3C0
		public string HomeAddressStreet
		{
			get
			{
				base.CheckDisposed();
				return null;
			}
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x06000D1D RID: 3357 RVA: 0x0001F1C9 File Offset: 0x0001D3C9
		public string HomeTelephoneNumber
		{
			get
			{
				base.CheckDisposed();
				return null;
			}
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x06000D1E RID: 3358 RVA: 0x0001F1D2 File Offset: 0x0001D3D2
		public string IMAddress
		{
			get
			{
				base.CheckDisposed();
				return null;
			}
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x06000D1F RID: 3359 RVA: 0x0001F1DB File Offset: 0x0001D3DB
		public string JobTitle
		{
			get
			{
				base.CheckDisposed();
				return this.jobTitle;
			}
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x06000D20 RID: 3360 RVA: 0x0001F1E9 File Offset: 0x0001D3E9
		public string LastName
		{
			get
			{
				base.CheckDisposed();
				return this.user.LastName;
			}
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x06000D21 RID: 3361 RVA: 0x0001F1FC File Offset: 0x0001D3FC
		public string Location
		{
			get
			{
				base.CheckDisposed();
				return null;
			}
		}

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06000D22 RID: 3362 RVA: 0x0001F205 File Offset: 0x0001D405
		public string MiddleName
		{
			get
			{
				base.CheckDisposed();
				return string.Empty;
			}
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x06000D23 RID: 3363 RVA: 0x0001F212 File Offset: 0x0001D412
		public string MobileTelephoneNumber
		{
			get
			{
				base.CheckDisposed();
				return null;
			}
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x06000D24 RID: 3364 RVA: 0x0001F21B File Offset: 0x0001D41B
		public byte[] OscContactSources
		{
			get
			{
				base.CheckDisposed();
				return this.oscContactSources;
			}
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x06000D25 RID: 3365 RVA: 0x0001F229 File Offset: 0x0001D429
		public string OtherTelephoneNumber
		{
			get
			{
				base.CheckDisposed();
				return null;
			}
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x06000D26 RID: 3366 RVA: 0x0001F232 File Offset: 0x0001D432
		public string PartnerNetworkContactType
		{
			get
			{
				base.CheckDisposed();
				return null;
			}
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x06000D27 RID: 3367 RVA: 0x0001F23B File Offset: 0x0001D43B
		public string PartnerNetworkId
		{
			get
			{
				base.CheckDisposed();
				return "Facebook";
			}
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x06000D28 RID: 3368 RVA: 0x0001F248 File Offset: 0x0001D448
		public string PartnerNetworkProfilePhotoUrl
		{
			get
			{
				base.CheckDisposed();
				return this.photoUrl;
			}
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x06000D29 RID: 3369 RVA: 0x0001F256 File Offset: 0x0001D456
		public string PartnerNetworkThumbnailPhotoUrl
		{
			get
			{
				base.CheckDisposed();
				return this.photoUrl;
			}
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06000D2A RID: 3370 RVA: 0x0001F264 File Offset: 0x0001D464
		public ExDateTime? PeopleConnectionCreationTime
		{
			get
			{
				base.CheckDisposed();
				return this.peopleConnectionCreationTime;
			}
		}

		// Token: 0x06000D2B RID: 3371 RVA: 0x0001F272 File Offset: 0x0001D472
		private void InitializePhotoUrl()
		{
			this.photoUrl = string.Format("https://graph.facebook.com/{0}/picture?type=large", this.user.Id);
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x06000D2C RID: 3372 RVA: 0x0001F28F File Offset: 0x0001D48F
		public string PartnerNetworkUserId
		{
			get
			{
				base.CheckDisposed();
				return this.user.Id;
			}
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x06000D2D RID: 3373 RVA: 0x0001F2A2 File Offset: 0x0001D4A2
		public string ProtectedEmailAddress
		{
			get
			{
				base.CheckDisposed();
				return this.user.EmailAddress;
			}
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x06000D2E RID: 3374 RVA: 0x0001F2B5 File Offset: 0x0001D4B5
		public string ProtectedPhoneNumber
		{
			get
			{
				base.CheckDisposed();
				return this.user.MobilePhoneNumber;
			}
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x06000D2F RID: 3375 RVA: 0x0001F2C8 File Offset: 0x0001D4C8
		public string Schools
		{
			get
			{
				base.CheckDisposed();
				return this.schools;
			}
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x06000D30 RID: 3376 RVA: 0x0001F2D6 File Offset: 0x0001D4D6
		public string Webpage
		{
			get
			{
				base.CheckDisposed();
				return this.user.ProfilePageUrl;
			}
		}

		// Token: 0x06000D31 RID: 3377 RVA: 0x0001F2E9 File Offset: 0x0001D4E9
		protected override void InternalDispose(bool disposing)
		{
		}

		// Token: 0x06000D32 RID: 3378 RVA: 0x0001F2EB File Offset: 0x0001D4EB
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<FacebookContact>(this);
		}

		// Token: 0x06000D33 RID: 3379 RVA: 0x0001F2F4 File Offset: 0x0001D4F4
		private void InitializeCompanyNameAndJobTitle()
		{
			if (this.user.WorkHistory == null || this.user.WorkHistory.Count == 0)
			{
				this.companyName = null;
				this.jobTitle = null;
				return;
			}
			FacebookWorkHistoryEntry facebookWorkHistoryEntry = this.FindCurrentJob() ?? this.user.WorkHistory[0];
			this.companyName = ((facebookWorkHistoryEntry.Employer != null) ? facebookWorkHistoryEntry.Employer.Name : null);
			this.jobTitle = ((facebookWorkHistoryEntry.Position != null) ? facebookWorkHistoryEntry.Position.Name : null);
		}

		// Token: 0x06000D34 RID: 3380 RVA: 0x0001F383 File Offset: 0x0001D583
		private FacebookWorkHistoryEntry FindCurrentJob()
		{
			return this.user.WorkHistory.FirstOrDefault(new Func<FacebookWorkHistoryEntry, bool>(this.IsCurrentJob));
		}

		// Token: 0x06000D35 RID: 3381 RVA: 0x0001F3A4 File Offset: 0x0001D5A4
		private bool IsCurrentJob(FacebookWorkHistoryEntry job)
		{
			return job != null && !string.IsNullOrWhiteSpace(job.StartDate) && !"0000-00".Equals(job.StartDate, StringComparison.OrdinalIgnoreCase) && (string.IsNullOrWhiteSpace(job.EndDate) || "0000-00".Equals(job.EndDate, StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x06000D36 RID: 3382 RVA: 0x0001F3FC File Offset: 0x0001D5FC
		private void InitializeBirthdate()
		{
			if (!string.IsNullOrEmpty(this.user.Birthday))
			{
				ExDateTime value;
				if (FacebookContact.DateTimeTryParseExact(this.user.Birthday, out value))
				{
					this.birthdate = new ExDateTime?(value);
					return;
				}
				if (FacebookContact.DateTimeTryParseExact(string.Format("{0}/{1}", this.user.Birthday, 1604), out value))
				{
					this.birthdate = new ExDateTime?(value);
				}
			}
		}

		// Token: 0x06000D37 RID: 3383 RVA: 0x0001F480 File Offset: 0x0001D680
		private void InitializeHobbies()
		{
			HashSet<string> hashSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			if (this.user.ActivitiesList != null && this.user.ActivitiesList.Activities != null)
			{
				hashSet.UnionWith(from a in this.user.ActivitiesList.Activities
				select a.Name);
			}
			if (this.user.InterestsList != null && this.user.InterestsList.Interests != null)
			{
				hashSet.UnionWith(from i in this.user.InterestsList.Interests
				select i.Name);
			}
			if (hashSet.Count == 0)
			{
				this.hobbies = null;
				return;
			}
			this.hobbies = string.Join(", ", hashSet.ToArray<string>());
		}

		// Token: 0x06000D38 RID: 3384 RVA: 0x0001F598 File Offset: 0x0001D798
		private void InitializeSchools()
		{
			if (this.user.EducationHistory == null)
			{
				this.schools = null;
				return;
			}
			string[] array = (from educationEntry in this.user.EducationHistory
			where educationEntry.School != null && !string.IsNullOrWhiteSpace(educationEntry.School.Name)
			select educationEntry.School.Name).ToArray<string>();
			if (array.Length == 0)
			{
				this.schools = null;
			}
			this.schools = string.Join(", ", array);
		}

		// Token: 0x06000D39 RID: 3385 RVA: 0x0001F62C File Offset: 0x0001D82C
		private void InitializeOscContactSources()
		{
			this.oscContactSources = OscContactSourcesForContactWriter.Instance.Write(OscProviderGuids.Facebook, "", this.user.Id);
		}

		// Token: 0x04000739 RID: 1849
		public const string FacebookPartnerNetworkId = "Facebook";

		// Token: 0x0400073A RID: 1850
		private const string PhotoUrlFormat = "https://graph.facebook.com/{0}/picture?type=large";

		// Token: 0x0400073B RID: 1851
		private readonly FacebookUser user;

		// Token: 0x0400073C RID: 1852
		private ExDateTime? birthdate;

		// Token: 0x0400073D RID: 1853
		private string hobbies;

		// Token: 0x0400073E RID: 1854
		private string companyName;

		// Token: 0x0400073F RID: 1855
		private string jobTitle;

		// Token: 0x04000740 RID: 1856
		private string schools;

		// Token: 0x04000741 RID: 1857
		private byte[] oscContactSources;

		// Token: 0x04000742 RID: 1858
		private readonly ExDateTime? peopleConnectionCreationTime;

		// Token: 0x04000743 RID: 1859
		private string photoUrl;
	}
}
