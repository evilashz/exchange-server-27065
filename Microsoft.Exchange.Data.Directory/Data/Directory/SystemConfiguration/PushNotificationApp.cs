using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using Microsoft.Exchange.Common;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200054C RID: 1356
	[Serializable]
	public class PushNotificationApp : ADConfigurationObject, IPushNotificationRawSettings, IEquatable<IPushNotificationRawSettings>, IEquatable<PushNotificationApp>
	{
		// Token: 0x17001353 RID: 4947
		// (get) Token: 0x06003CBE RID: 15550 RVA: 0x000E7F87 File Offset: 0x000E6187
		internal override ADObjectId ParentPath
		{
			get
			{
				return PushNotificationApp.RdnContainer;
			}
		}

		// Token: 0x17001354 RID: 4948
		// (get) Token: 0x06003CBF RID: 15551 RVA: 0x000E7F8E File Offset: 0x000E618E
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2012;
			}
		}

		// Token: 0x17001355 RID: 4949
		// (get) Token: 0x06003CC0 RID: 15552 RVA: 0x000E7F95 File Offset: 0x000E6195
		internal override ADObjectSchema Schema
		{
			get
			{
				return PushNotificationApp.schema;
			}
		}

		// Token: 0x17001356 RID: 4950
		// (get) Token: 0x06003CC1 RID: 15553 RVA: 0x000E7F9C File Offset: 0x000E619C
		internal override string MostDerivedObjectClass
		{
			get
			{
				return "msExchPushNotificationsApp";
			}
		}

		// Token: 0x17001357 RID: 4951
		// (get) Token: 0x06003CC2 RID: 15554 RVA: 0x000E7FA3 File Offset: 0x000E61A3
		// (set) Token: 0x06003CC3 RID: 15555 RVA: 0x000E7FB5 File Offset: 0x000E61B5
		public DateTime? LastUpdateTimeUtc
		{
			get
			{
				return (DateTime?)this[PushNotificationAppSchema.LastUpdateTimeUtc];
			}
			set
			{
				this[PushNotificationAppSchema.LastUpdateTimeUtc] = value;
			}
		}

		// Token: 0x17001358 RID: 4952
		// (get) Token: 0x06003CC4 RID: 15556 RVA: 0x000E7FC8 File Offset: 0x000E61C8
		// (set) Token: 0x06003CC5 RID: 15557 RVA: 0x000E7FD0 File Offset: 0x000E61D0
		public new string Name
		{
			get
			{
				return base.Name;
			}
			set
			{
				base.Name = value;
			}
		}

		// Token: 0x17001359 RID: 4953
		// (get) Token: 0x06003CC6 RID: 15558 RVA: 0x000E7FD9 File Offset: 0x000E61D9
		// (set) Token: 0x06003CC7 RID: 15559 RVA: 0x000E7FEB File Offset: 0x000E61EB
		[Parameter]
		public string DisplayName
		{
			get
			{
				return (string)this[PushNotificationAppSchema.DisplayName];
			}
			set
			{
				this[PushNotificationAppSchema.DisplayName] = value;
			}
		}

		// Token: 0x1700135A RID: 4954
		// (get) Token: 0x06003CC8 RID: 15560 RVA: 0x000E7FF9 File Offset: 0x000E61F9
		// (set) Token: 0x06003CC9 RID: 15561 RVA: 0x000E800B File Offset: 0x000E620B
		public PushNotificationPlatform Platform
		{
			get
			{
				return (PushNotificationPlatform)this[PushNotificationAppSchema.Platform];
			}
			set
			{
				this[PushNotificationAppSchema.Platform] = value;
			}
		}

		// Token: 0x1700135B RID: 4955
		// (get) Token: 0x06003CCA RID: 15562 RVA: 0x000E801E File Offset: 0x000E621E
		// (set) Token: 0x06003CCB RID: 15563 RVA: 0x000E8030 File Offset: 0x000E6230
		[Parameter]
		public bool? Enabled
		{
			get
			{
				return (bool?)this[PushNotificationAppSchema.Enabled];
			}
			set
			{
				this[PushNotificationAppSchema.Enabled] = value;
			}
		}

		// Token: 0x1700135C RID: 4956
		// (get) Token: 0x06003CCC RID: 15564 RVA: 0x000E8043 File Offset: 0x000E6243
		// (set) Token: 0x06003CCD RID: 15565 RVA: 0x000E8055 File Offset: 0x000E6255
		[Parameter]
		public Version ExchangeMinimumVersion
		{
			get
			{
				return (Version)this[PushNotificationAppSchema.ExchangeMinimumVersion];
			}
			set
			{
				this[PushNotificationAppSchema.ExchangeMinimumVersion] = value;
			}
		}

		// Token: 0x1700135D RID: 4957
		// (get) Token: 0x06003CCE RID: 15566 RVA: 0x000E8063 File Offset: 0x000E6263
		// (set) Token: 0x06003CCF RID: 15567 RVA: 0x000E8075 File Offset: 0x000E6275
		[Parameter]
		public Version ExchangeMaximumVersion
		{
			get
			{
				return (Version)this[PushNotificationAppSchema.ExchangeMaximumVersion];
			}
			set
			{
				this[PushNotificationAppSchema.ExchangeMaximumVersion] = value;
			}
		}

		// Token: 0x1700135E RID: 4958
		// (get) Token: 0x06003CD0 RID: 15568 RVA: 0x000E8083 File Offset: 0x000E6283
		// (set) Token: 0x06003CD1 RID: 15569 RVA: 0x000E8095 File Offset: 0x000E6295
		[Parameter]
		public int? QueueSize
		{
			get
			{
				return (int?)this[PushNotificationAppSchema.QueueSize];
			}
			set
			{
				this[PushNotificationAppSchema.QueueSize] = value;
			}
		}

		// Token: 0x1700135F RID: 4959
		// (get) Token: 0x06003CD2 RID: 15570 RVA: 0x000E80A8 File Offset: 0x000E62A8
		// (set) Token: 0x06003CD3 RID: 15571 RVA: 0x000E80BA File Offset: 0x000E62BA
		[Parameter]
		public int? NumberOfChannels
		{
			get
			{
				return (int?)this[PushNotificationAppSchema.NumberOfChannels];
			}
			set
			{
				this[PushNotificationAppSchema.NumberOfChannels] = value;
			}
		}

		// Token: 0x17001360 RID: 4960
		// (get) Token: 0x06003CD4 RID: 15572 RVA: 0x000E80CD File Offset: 0x000E62CD
		// (set) Token: 0x06003CD5 RID: 15573 RVA: 0x000E80DF File Offset: 0x000E62DF
		public string AuthenticationId
		{
			get
			{
				return (string)this[PushNotificationAppSchema.AuthenticationId];
			}
			set
			{
				this[PushNotificationAppSchema.AuthenticationId] = value;
			}
		}

		// Token: 0x17001361 RID: 4961
		// (get) Token: 0x06003CD6 RID: 15574 RVA: 0x000E80ED File Offset: 0x000E62ED
		// (set) Token: 0x06003CD7 RID: 15575 RVA: 0x000E80FF File Offset: 0x000E62FF
		public string AuthenticationKey
		{
			get
			{
				return (string)this[PushNotificationAppSchema.AuthenticationKey];
			}
			set
			{
				this[PushNotificationAppSchema.AuthenticationKey] = value;
			}
		}

		// Token: 0x17001362 RID: 4962
		// (get) Token: 0x06003CD8 RID: 15576 RVA: 0x000E810D File Offset: 0x000E630D
		// (set) Token: 0x06003CD9 RID: 15577 RVA: 0x000E811F File Offset: 0x000E631F
		public string AuthenticationKeyFallback
		{
			get
			{
				return (string)this[PushNotificationAppSchema.AuthenticationKeyFallback];
			}
			set
			{
				this[PushNotificationAppSchema.AuthenticationKeyFallback] = value;
			}
		}

		// Token: 0x17001363 RID: 4963
		// (get) Token: 0x06003CDA RID: 15578 RVA: 0x000E812D File Offset: 0x000E632D
		// (set) Token: 0x06003CDB RID: 15579 RVA: 0x000E813F File Offset: 0x000E633F
		public bool? IsAuthenticationKeyEncrypted
		{
			get
			{
				return (bool?)this[PushNotificationAppSchema.IsAuthenticationKeyEncrypted];
			}
			set
			{
				this[PushNotificationAppSchema.IsAuthenticationKeyEncrypted] = value;
			}
		}

		// Token: 0x17001364 RID: 4964
		// (get) Token: 0x06003CDC RID: 15580 RVA: 0x000E8152 File Offset: 0x000E6352
		// (set) Token: 0x06003CDD RID: 15581 RVA: 0x000E8164 File Offset: 0x000E6364
		public string Url
		{
			get
			{
				return (string)this[PushNotificationAppSchema.Url];
			}
			set
			{
				this[PushNotificationAppSchema.Url] = value;
			}
		}

		// Token: 0x17001365 RID: 4965
		// (get) Token: 0x06003CDE RID: 15582 RVA: 0x000E8172 File Offset: 0x000E6372
		// (set) Token: 0x06003CDF RID: 15583 RVA: 0x000E8184 File Offset: 0x000E6384
		public string UriTemplate
		{
			get
			{
				return (string)this[PushNotificationAppSchema.UriTemplate];
			}
			set
			{
				this[PushNotificationAppSchema.UriTemplate] = value;
			}
		}

		// Token: 0x17001366 RID: 4966
		// (get) Token: 0x06003CE0 RID: 15584 RVA: 0x000E8192 File Offset: 0x000E6392
		// (set) Token: 0x06003CE1 RID: 15585 RVA: 0x000E81A4 File Offset: 0x000E63A4
		public int? Port
		{
			get
			{
				return (int?)this[PushNotificationAppSchema.Port];
			}
			set
			{
				this[PushNotificationAppSchema.Port] = value;
			}
		}

		// Token: 0x17001367 RID: 4967
		// (get) Token: 0x06003CE2 RID: 15586 RVA: 0x000E81B7 File Offset: 0x000E63B7
		// (set) Token: 0x06003CE3 RID: 15587 RVA: 0x000E81C9 File Offset: 0x000E63C9
		public string SecondaryUrl
		{
			get
			{
				return (string)this[PushNotificationAppSchema.SecondaryUrl];
			}
			set
			{
				this[PushNotificationAppSchema.SecondaryUrl] = value;
			}
		}

		// Token: 0x17001368 RID: 4968
		// (get) Token: 0x06003CE4 RID: 15588 RVA: 0x000E81D7 File Offset: 0x000E63D7
		// (set) Token: 0x06003CE5 RID: 15589 RVA: 0x000E81E9 File Offset: 0x000E63E9
		public int? SecondaryPort
		{
			get
			{
				return (int?)this[PushNotificationAppSchema.SecondaryPort];
			}
			set
			{
				this[PushNotificationAppSchema.SecondaryPort] = value;
			}
		}

		// Token: 0x17001369 RID: 4969
		// (get) Token: 0x06003CE6 RID: 15590 RVA: 0x000E81FC File Offset: 0x000E63FC
		// (set) Token: 0x06003CE7 RID: 15591 RVA: 0x000E820E File Offset: 0x000E640E
		[Parameter]
		public int? BackOffTimeInSeconds
		{
			get
			{
				return (int?)this[PushNotificationAppSchema.BackOffTimeInSeconds];
			}
			set
			{
				this[PushNotificationAppSchema.BackOffTimeInSeconds] = value;
			}
		}

		// Token: 0x1700136A RID: 4970
		// (get) Token: 0x06003CE8 RID: 15592 RVA: 0x000E8224 File Offset: 0x000E6424
		public int? AddTimeout
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700136B RID: 4971
		// (get) Token: 0x06003CE9 RID: 15593 RVA: 0x000E823C File Offset: 0x000E643C
		public int? ConnectStepTimeout
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700136C RID: 4972
		// (get) Token: 0x06003CEA RID: 15594 RVA: 0x000E8254 File Offset: 0x000E6454
		public int? ConnectTotalTimeout
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700136D RID: 4973
		// (get) Token: 0x06003CEB RID: 15595 RVA: 0x000E826C File Offset: 0x000E646C
		public int? ConnectRetryDelay
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700136E RID: 4974
		// (get) Token: 0x06003CEC RID: 15596 RVA: 0x000E8284 File Offset: 0x000E6484
		public int? AuthenticateRetryMax
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700136F RID: 4975
		// (get) Token: 0x06003CED RID: 15597 RVA: 0x000E829C File Offset: 0x000E649C
		public int? ConnectRetryMax
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001370 RID: 4976
		// (get) Token: 0x06003CEE RID: 15598 RVA: 0x000E82B4 File Offset: 0x000E64B4
		public int? ReadTimeout
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001371 RID: 4977
		// (get) Token: 0x06003CEF RID: 15599 RVA: 0x000E82CC File Offset: 0x000E64CC
		public int? WriteTimeout
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001372 RID: 4978
		// (get) Token: 0x06003CF0 RID: 15600 RVA: 0x000E82E4 File Offset: 0x000E64E4
		public bool? IgnoreCertificateErrors
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001373 RID: 4979
		// (get) Token: 0x06003CF1 RID: 15601 RVA: 0x000E82FC File Offset: 0x000E64FC
		public int? MaximumCacheSize
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001374 RID: 4980
		// (get) Token: 0x06003CF2 RID: 15602 RVA: 0x000E8312 File Offset: 0x000E6512
		// (set) Token: 0x06003CF3 RID: 15603 RVA: 0x000E8324 File Offset: 0x000E6524
		public string RegistrationTemplate
		{
			get
			{
				return (string)this[PushNotificationAppSchema.RegistrationTemplate];
			}
			set
			{
				this[PushNotificationAppSchema.RegistrationTemplate] = value;
			}
		}

		// Token: 0x17001375 RID: 4981
		// (get) Token: 0x06003CF4 RID: 15604 RVA: 0x000E8332 File Offset: 0x000E6532
		// (set) Token: 0x06003CF5 RID: 15605 RVA: 0x000E8344 File Offset: 0x000E6544
		public bool? RegistrationEnabled
		{
			get
			{
				return (bool?)this[PushNotificationAppSchema.RegistrationEnabled];
			}
			set
			{
				this[PushNotificationAppSchema.RegistrationEnabled] = value;
			}
		}

		// Token: 0x17001376 RID: 4982
		// (get) Token: 0x06003CF6 RID: 15606 RVA: 0x000E8357 File Offset: 0x000E6557
		// (set) Token: 0x06003CF7 RID: 15607 RVA: 0x000E8369 File Offset: 0x000E6569
		public bool? MultifactorRegistrationEnabled
		{
			get
			{
				return (bool?)this[PushNotificationAppSchema.MultifactorRegistrationEnabled];
			}
			set
			{
				this[PushNotificationAppSchema.MultifactorRegistrationEnabled] = value;
			}
		}

		// Token: 0x17001377 RID: 4983
		// (get) Token: 0x06003CF8 RID: 15608 RVA: 0x000E837C File Offset: 0x000E657C
		// (set) Token: 0x06003CF9 RID: 15609 RVA: 0x000E838E File Offset: 0x000E658E
		public string PartitionName
		{
			get
			{
				return (string)this[PushNotificationAppSchema.PartitionName];
			}
			set
			{
				this[PushNotificationAppSchema.PartitionName] = value;
			}
		}

		// Token: 0x17001378 RID: 4984
		// (get) Token: 0x06003CFA RID: 15610 RVA: 0x000E839C File Offset: 0x000E659C
		// (set) Token: 0x06003CFB RID: 15611 RVA: 0x000E83AE File Offset: 0x000E65AE
		public bool? IsDefaultPartitionName
		{
			get
			{
				return (bool?)this[PushNotificationAppSchema.IsDefaultPartitionName];
			}
			set
			{
				this[PushNotificationAppSchema.IsDefaultPartitionName] = value;
			}
		}

		// Token: 0x06003CFC RID: 15612 RVA: 0x000E83C4 File Offset: 0x000E65C4
		public string ToFullString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (ADPropertyDefinition adpropertyDefinition in PushNotificationApp.ComparableProperties.Value)
			{
				object obj = null;
				this.propertyBag.TryGetField(adpropertyDefinition, ref obj);
				string text = null;
				if (obj != null)
				{
					if (!adpropertyDefinition.IsMultivalued)
					{
						text = ADValueConvertor.ConvertValueToString(obj, adpropertyDefinition.FormatProvider);
					}
					else
					{
						MultiValuedPropertyBase multiValuedPropertyBase = obj as MultiValuedPropertyBase;
						StringBuilder stringBuilder2 = new StringBuilder();
						foreach (object originalValue in ((IEnumerable)multiValuedPropertyBase))
						{
							if (stringBuilder2.Length > 0)
							{
								stringBuilder2.Append(", ");
							}
							stringBuilder2.AppendFormat("{0}", ADValueConvertor.ConvertValueToString(originalValue, adpropertyDefinition.FormatProvider));
						}
						text = string.Format("@({0})", stringBuilder2.ToString());
					}
				}
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append("; ");
				}
				stringBuilder.AppendFormat("{0}:{1}", adpropertyDefinition.Name, (text != null) ? text : "<null>");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003CFD RID: 15613 RVA: 0x000E8534 File Offset: 0x000E6734
		public override bool Equals(object obj)
		{
			return obj is PushNotificationApp && this.Equals((PushNotificationApp)obj);
		}

		// Token: 0x06003CFE RID: 15614 RVA: 0x000E854C File Offset: 0x000E674C
		public bool Equals(IPushNotificationRawSettings other)
		{
			return other is PushNotificationApp && this.Equals((PushNotificationApp)other);
		}

		// Token: 0x06003CFF RID: 15615 RVA: 0x000E8564 File Offset: 0x000E6764
		public bool Equals(PushNotificationApp other)
		{
			return DeepADObjectEqualityComparer.Instance.Equals(this, other, PushNotificationApp.ComparableProperties.Value);
		}

		// Token: 0x06003D00 RID: 15616 RVA: 0x000E857C File Offset: 0x000E677C
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06003D01 RID: 15617 RVA: 0x000E8584 File Offset: 0x000E6784
		protected override void ValidateWrite(List<ValidationError> errors)
		{
			base.ValidateWrite(errors);
			if (this.Platform == PushNotificationPlatform.None)
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.ErrorInvalidPushNotificationPlatform, PushNotificationAppSchema.Platform, this.Platform));
			}
		}

		// Token: 0x06003D02 RID: 15618 RVA: 0x000E85B8 File Offset: 0x000E67B8
		private static IEnumerable<ADPropertyDefinition> CreateComparableProperties()
		{
			List<ADPropertyDefinition> list = new List<ADPropertyDefinition>();
			foreach (PropertyDefinition propertyDefinition in PushNotificationApp.schema.AllProperties)
			{
				ADPropertyDefinition adpropertyDefinition = (ADPropertyDefinition)propertyDefinition;
				if (adpropertyDefinition.LdapDisplayName != null && !adpropertyDefinition.IsCalculated && !PushNotificationApp.ExcludedProperties.Contains(adpropertyDefinition.LdapDisplayName, StringComparer.Ordinal))
				{
					list.Add(adpropertyDefinition);
				}
			}
			return list;
		}

		// Token: 0x04002934 RID: 10548
		private const string mostDerivedClass = "msExchPushNotificationsApp";

		// Token: 0x04002935 RID: 10549
		public const string PushNotificationAppContainerName = "Push Notifications Settings";

		// Token: 0x04002936 RID: 10550
		private static readonly string[] ExcludedProperties = new string[]
		{
			ADObjectSchema.Id.LdapDisplayName,
			ADObjectSchema.WhenChangedRaw.LdapDisplayName,
			ADObjectSchema.WhenCreatedRaw.LdapDisplayName
		};

		// Token: 0x04002937 RID: 10551
		private static PushNotificationAppSchema schema = ObjectSchema.GetInstance<PushNotificationAppSchema>();

		// Token: 0x04002938 RID: 10552
		private static Lazy<IEnumerable<ADPropertyDefinition>> ComparableProperties = new Lazy<IEnumerable<ADPropertyDefinition>>(() => PushNotificationApp.CreateComparableProperties());

		// Token: 0x04002939 RID: 10553
		internal static readonly ADObjectId RdnContainer = new ADObjectId(string.Format("CN={0}", "Push Notifications Settings"));
	}
}
