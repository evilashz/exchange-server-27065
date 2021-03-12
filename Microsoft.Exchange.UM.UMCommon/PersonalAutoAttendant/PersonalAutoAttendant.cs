using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.InfoWorker.Common.Availability;

namespace Microsoft.Exchange.UM.PersonalAutoAttendant
{
	// Token: 0x02000105 RID: 261
	internal class PersonalAutoAttendant : IComparable<PersonalAutoAttendant>, IEquatable<PersonalAutoAttendant>
	{
		// Token: 0x06000856 RID: 2134 RVA: 0x0002015E File Offset: 0x0001E35E
		private PersonalAutoAttendant()
		{
			this.keyMappingList = new KeyMappings();
			this.autoActionsList = new KeyMappings();
			this.callerIdList = new List<CallerIdBase>();
			this.extensionList = new ExtensionList();
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000857 RID: 2135 RVA: 0x00020192 File Offset: 0x0001E392
		// (set) Token: 0x06000858 RID: 2136 RVA: 0x0002019A File Offset: 0x0001E39A
		internal Version Version
		{
			get
			{
				return this.version;
			}
			set
			{
				this.version = value;
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000859 RID: 2137 RVA: 0x000201A3 File Offset: 0x0001E3A3
		// (set) Token: 0x0600085A RID: 2138 RVA: 0x000201AB File Offset: 0x0001E3AB
		internal string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x0600085B RID: 2139 RVA: 0x000201B4 File Offset: 0x0001E3B4
		// (set) Token: 0x0600085C RID: 2140 RVA: 0x000201BC File Offset: 0x0001E3BC
		internal Guid Identity
		{
			get
			{
				return this.identity;
			}
			set
			{
				this.identity = value;
				this.greeting = this.identity.ToString("N");
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x0600085D RID: 2141 RVA: 0x000201DB File Offset: 0x0001E3DB
		// (set) Token: 0x0600085E RID: 2142 RVA: 0x000201E3 File Offset: 0x0001E3E3
		internal bool Enabled
		{
			get
			{
				return this.enabled;
			}
			set
			{
				this.enabled = value;
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x0600085F RID: 2143 RVA: 0x000201EC File Offset: 0x0001E3EC
		// (set) Token: 0x06000860 RID: 2144 RVA: 0x000201F4 File Offset: 0x0001E3F4
		internal ExtensionList ExtensionList
		{
			get
			{
				return this.extensionList;
			}
			set
			{
				this.extensionList = value;
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000861 RID: 2145 RVA: 0x000201FD File Offset: 0x0001E3FD
		// (set) Token: 0x06000862 RID: 2146 RVA: 0x00020205 File Offset: 0x0001E405
		internal List<CallerIdBase> CallerIdList
		{
			get
			{
				return this.callerIdList;
			}
			set
			{
				this.callerIdList = value;
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000863 RID: 2147 RVA: 0x0002020E File Offset: 0x0001E40E
		// (set) Token: 0x06000864 RID: 2148 RVA: 0x00020216 File Offset: 0x0001E416
		internal bool Valid
		{
			get
			{
				return this.valid;
			}
			set
			{
				this.valid = value;
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000865 RID: 2149 RVA: 0x0002021F File Offset: 0x0001E41F
		// (set) Token: 0x06000866 RID: 2150 RVA: 0x00020227 File Offset: 0x0001E427
		internal TimeOfDayEnum TimeOfDay
		{
			get
			{
				return this.timeOfDay;
			}
			set
			{
				this.timeOfDay = value;
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000867 RID: 2151 RVA: 0x00020230 File Offset: 0x0001E430
		// (set) Token: 0x06000868 RID: 2152 RVA: 0x00020238 File Offset: 0x0001E438
		internal WorkingPeriod WorkingPeriod
		{
			get
			{
				return this.workingPeriod;
			}
			set
			{
				this.workingPeriod = value;
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000869 RID: 2153 RVA: 0x00020241 File Offset: 0x0001E441
		// (set) Token: 0x0600086A RID: 2154 RVA: 0x00020249 File Offset: 0x0001E449
		internal FreeBusyStatusEnum FreeBusy
		{
			get
			{
				return this.freeBusy;
			}
			set
			{
				this.freeBusy = value;
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x0600086B RID: 2155 RVA: 0x00020252 File Offset: 0x0001E452
		// (set) Token: 0x0600086C RID: 2156 RVA: 0x0002025A File Offset: 0x0001E45A
		internal OutOfOfficeStatusEnum OutOfOffice
		{
			get
			{
				return this.outOfOffice;
			}
			set
			{
				this.outOfOffice = value;
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x0600086D RID: 2157 RVA: 0x00020263 File Offset: 0x0001E463
		// (set) Token: 0x0600086E RID: 2158 RVA: 0x0002026B File Offset: 0x0001E46B
		internal bool EnableBargeIn
		{
			get
			{
				return this.enableBargeIn;
			}
			set
			{
				this.enableBargeIn = value;
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x0600086F RID: 2159 RVA: 0x00020274 File Offset: 0x0001E474
		// (set) Token: 0x06000870 RID: 2160 RVA: 0x0002027C File Offset: 0x0001E47C
		internal string Greeting
		{
			get
			{
				return this.greeting;
			}
			set
			{
				this.greeting = value;
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000871 RID: 2161 RVA: 0x00020285 File Offset: 0x0001E485
		// (set) Token: 0x06000872 RID: 2162 RVA: 0x0002028D File Offset: 0x0001E48D
		internal string OwaPreview
		{
			get
			{
				return this.owaPreview;
			}
			set
			{
				this.owaPreview = value;
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000873 RID: 2163 RVA: 0x00020296 File Offset: 0x0001E496
		// (set) Token: 0x06000874 RID: 2164 RVA: 0x0002029E File Offset: 0x0001E49E
		internal bool IsCompatible
		{
			get
			{
				return this.paaIsOfCurrentVersion;
			}
			set
			{
				this.paaIsOfCurrentVersion = value;
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000875 RID: 2165 RVA: 0x000202A7 File Offset: 0x0001E4A7
		// (set) Token: 0x06000876 RID: 2166 RVA: 0x000202AF File Offset: 0x0001E4AF
		internal KeyMappings KeyMappingList
		{
			get
			{
				return this.keyMappingList;
			}
			set
			{
				this.keyMappingList = value;
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000877 RID: 2167 RVA: 0x000202B8 File Offset: 0x0001E4B8
		// (set) Token: 0x06000878 RID: 2168 RVA: 0x000202C0 File Offset: 0x0001E4C0
		internal KeyMappings AutoActionsList
		{
			get
			{
				return this.autoActionsList;
			}
			set
			{
				this.autoActionsList = value;
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000879 RID: 2169 RVA: 0x000202C9 File Offset: 0x0001E4C9
		// (set) Token: 0x0600087A RID: 2170 RVA: 0x000202D1 File Offset: 0x0001E4D1
		internal XmlNode[] Unprocessed
		{
			get
			{
				return this.unprocessed;
			}
			set
			{
				this.unprocessed = value;
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x0600087B RID: 2171 RVA: 0x000202DA File Offset: 0x0001E4DA
		// (set) Token: 0x0600087C RID: 2172 RVA: 0x000202E2 File Offset: 0x0001E4E2
		internal List<XmlNode> DocumentNodes
		{
			get
			{
				return this.rawNodes;
			}
			set
			{
				this.rawNodes = value;
			}
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x000202EC File Offset: 0x0001E4EC
		public int CompareTo(PersonalAutoAttendant other)
		{
			return this.Identity.CompareTo(other.Identity);
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x00020310 File Offset: 0x0001E510
		public bool Equals(PersonalAutoAttendant other)
		{
			return this.Identity.Equals(other.Identity);
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x00020334 File Offset: 0x0001E534
		public bool Validate(IDataValidator dataValidator, PAAValidationMode validationMode)
		{
			this.valid = true;
			if (validationMode != PAAValidationMode.Actions)
			{
				this.valid = this.extensionList.Validate(dataValidator);
				if (!this.valid && validationMode == PAAValidationMode.StopOnFirstError)
				{
					return false;
				}
				for (int i = 0; i < this.CallerIdList.Count; i++)
				{
					CallerIdBase callerIdBase = this.CallerIdList[i];
					if (!callerIdBase.Validate(dataValidator))
					{
						this.valid = false;
						if (validationMode == PAAValidationMode.StopOnFirstError)
						{
							break;
						}
					}
				}
				if (!this.valid && validationMode == PAAValidationMode.StopOnFirstError)
				{
					return false;
				}
			}
			this.ValidateKeyMappings(this.autoActionsList, dataValidator, validationMode);
			if (!this.valid && validationMode == PAAValidationMode.StopOnFirstError)
			{
				return false;
			}
			this.ValidateKeyMappings(this.keyMappingList, dataValidator, validationMode);
			return (this.valid || validationMode != PAAValidationMode.StopOnFirstError) && this.valid;
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x000203F4 File Offset: 0x0001E5F4
		public void ValidateKeyMappings(KeyMappings keyMappings, IDataValidator dataValidator, PAAValidationMode validationMode)
		{
			for (int i = 0; i < keyMappings.Count; i++)
			{
				KeyMappingBase keyMappingBase = keyMappings.Menu[i];
				if (!keyMappingBase.Validate(dataValidator))
				{
					this.valid = false;
					if (validationMode == PAAValidationMode.StopOnFirstError)
					{
						return;
					}
				}
			}
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x00020434 File Offset: 0x0001E634
		internal static PersonalAutoAttendant CreateNew()
		{
			PersonalAutoAttendant personalAutoAttendant = new PersonalAutoAttendant();
			personalAutoAttendant.Version = PAAConstants.CurrentVersion;
			personalAutoAttendant.IsCompatible = true;
			personalAutoAttendant.Identity = Guid.NewGuid();
			personalAutoAttendant.EnableBargeIn = true;
			personalAutoAttendant.KeyMappingList.AddTransferToVoicemail(null);
			return personalAutoAttendant;
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x00020478 File Offset: 0x0001E678
		internal static PersonalAutoAttendant CreateUninitialized()
		{
			return new PersonalAutoAttendant();
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x0002047F File Offset: 0x0001E67F
		internal void AddPhoneNumberCallerId(string phone)
		{
			this.AddCallerId(new PhoneNumberCallerId(phone));
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x0002048D File Offset: 0x0001E68D
		internal void AddADContactCallerId(string legacyExchangeDN)
		{
			this.AddCallerId(new ADContactCallerId(legacyExchangeDN));
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x0002049B File Offset: 0x0001E69B
		internal void AddPersonaContactCallerId(EmailAddress emailAddress)
		{
			this.AddCallerId(new PersonaContactCallerId(emailAddress));
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x000204AC File Offset: 0x0001E6AC
		internal void AddPersonaContactCallerId(string emailAddressWithDisplayName)
		{
			string[] array = emailAddressWithDisplayName.Split(new string[]
			{
				":"
			}, StringSplitOptions.RemoveEmptyEntries);
			EmailAddress emailAddress = new EmailAddress();
			if (array != null)
			{
				switch (array.Length)
				{
				case 1:
					emailAddress.Address = array[0];
					break;
				case 2:
					emailAddress.Address = array[0];
					emailAddress.Name = array[1];
					break;
				}
				this.AddPersonaContactCallerId(emailAddress);
			}
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x00020514 File Offset: 0x0001E714
		internal void AddDefaultContactFolderCallerId()
		{
			this.AddCallerId(new ContactFolderCallerId());
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x00020521 File Offset: 0x0001E721
		internal void AddContactItemCallerId(string base64String)
		{
			this.AddContactItemCallerId(StoreObjectId.Deserialize(base64String));
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x0002052F File Offset: 0x0001E72F
		internal void AddContactItemCallerId(StoreObjectId storeObjectId)
		{
			this.AddContactItemCallerId(new ContactItemCallerId(storeObjectId));
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x0002053D File Offset: 0x0001E73D
		internal void AddContactItemCallerId(ContactItemCallerId callerId)
		{
			this.AddCallerId(callerId);
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x00020546 File Offset: 0x0001E746
		private void AddCallerId(CallerIdBase callerId)
		{
			this.callerIdList.Add(callerId);
		}

		// Token: 0x040004D1 RID: 1233
		private Version version;

		// Token: 0x040004D2 RID: 1234
		private string name;

		// Token: 0x040004D3 RID: 1235
		private Guid identity;

		// Token: 0x040004D4 RID: 1236
		private bool enabled;

		// Token: 0x040004D5 RID: 1237
		private bool paaIsOfCurrentVersion;

		// Token: 0x040004D6 RID: 1238
		private bool valid;

		// Token: 0x040004D7 RID: 1239
		private ExtensionList extensionList;

		// Token: 0x040004D8 RID: 1240
		private List<CallerIdBase> callerIdList;

		// Token: 0x040004D9 RID: 1241
		private TimeOfDayEnum timeOfDay;

		// Token: 0x040004DA RID: 1242
		private WorkingPeriod workingPeriod;

		// Token: 0x040004DB RID: 1243
		private FreeBusyStatusEnum freeBusy;

		// Token: 0x040004DC RID: 1244
		private OutOfOfficeStatusEnum outOfOffice;

		// Token: 0x040004DD RID: 1245
		private bool enableBargeIn;

		// Token: 0x040004DE RID: 1246
		private string greeting;

		// Token: 0x040004DF RID: 1247
		private string owaPreview;

		// Token: 0x040004E0 RID: 1248
		private KeyMappings keyMappingList;

		// Token: 0x040004E1 RID: 1249
		private KeyMappings autoActionsList;

		// Token: 0x040004E2 RID: 1250
		private XmlNode[] unprocessed;

		// Token: 0x040004E3 RID: 1251
		private List<XmlNode> rawNodes;
	}
}
