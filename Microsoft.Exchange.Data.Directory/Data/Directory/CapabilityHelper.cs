using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x020000C1 RID: 193
	internal static class CapabilityHelper
	{
		// Token: 0x060009E9 RID: 2537 RVA: 0x0002CB48 File Offset: 0x0002AD48
		internal static Capability? GetSKUCapability(MultiValuedProperty<Capability> capabilities)
		{
			foreach (Capability capability in capabilities)
			{
				if (CapabilityHelper.RootSKUCapabilities.Contains(capability))
				{
					return new Capability?(capability);
				}
			}
			return null;
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x0002CBB0 File Offset: 0x0002ADB0
		internal static string TransformCapabilityString(string capabilityString)
		{
			if (CapabilityHelper.ffoOffers.Contains(capabilityString.ToUpper()))
			{
				return "BPOS_S_EopStandardAddOn";
			}
			if (CapabilityHelper.ffoPremiumOffers.Contains(capabilityString.ToUpper()))
			{
				return "BPOS_S_EopPremiumAddOn";
			}
			return capabilityString;
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x0002CBE4 File Offset: 0x0002ADE4
		internal static void SetSKUCapability(Capability? skuCapability, MultiValuedProperty<Capability> capabilities)
		{
			if (skuCapability != null && !CapabilityHelper.RootSKUCapabilities.Contains(skuCapability.Value))
			{
				throw new ArgumentOutOfRangeException("skuCapability", skuCapability.Value, DirectoryStrings.ExArgumentOutOfRangeException("skuCapability", skuCapability.Value));
			}
			if (skuCapability != null && capabilities.Contains(skuCapability.Value))
			{
				return;
			}
			if (capabilities.Count > 0)
			{
				foreach (Capability item in CapabilityHelper.RootSKUCapabilities)
				{
					capabilities.Remove(item);
					if (capabilities.Count == 0)
					{
						break;
					}
				}
			}
			if (skuCapability != null)
			{
				capabilities.Add(skuCapability.Value);
			}
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x0002CCC8 File Offset: 0x0002AEC8
		internal static void SetSKUCapabilities(string sourceName, MultiValuedProperty<Capability> sourceCapabilities, MultiValuedProperty<Capability> targetCapabilities)
		{
			List<Capability> rootSKUCapabilities = CapabilityHelper.GetRootSKUCapabilities(sourceCapabilities);
			CapabilityHelper.SetSKUCapability((rootSKUCapabilities.Count == 0) ? null : new Capability?(rootSKUCapabilities[0]), targetCapabilities);
			foreach (Capability item in sourceCapabilities)
			{
				if (CapabilityHelper.AddOnSKUCapabilities.Contains(item) && !targetCapabilities.Contains(item))
				{
					targetCapabilities.Add(item);
				}
			}
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x0002CD78 File Offset: 0x0002AF78
		internal static void SetAddOnSKUCapabilities(MultiValuedProperty<Capability> sourceCapabilities, MultiValuedProperty<Capability> targetCapabilities)
		{
			if (!sourceCapabilities.All((Capability c) => CapabilityHelper.IsAddOnSKUCapability(c)))
			{
				throw new ArgumentOutOfRangeException("sourceCapabilities", sourceCapabilities, DirectoryStrings.ExArgumentOutOfRangeException("sourceCapabilities", sourceCapabilities));
			}
			foreach (Capability item in from c in CapabilityHelper.AddOnSKUCapabilities
			where targetCapabilities.Contains(c)
			select c)
			{
				targetCapabilities.Remove(item);
			}
			foreach (Capability item2 in sourceCapabilities)
			{
				targetCapabilities.Add(item2);
			}
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x0002CE8C File Offset: 0x0002B08C
		internal static void SetTenantSKUCapabilities(MultiValuedProperty<Capability> sourceCapabilities, MultiValuedProperty<Capability> targetCapabilities)
		{
			if (!sourceCapabilities.All((Capability c) => CapabilityHelper.IsAllowedSKUCapability(c)))
			{
				throw new ArgumentOutOfRangeException("sourceCapabilities", sourceCapabilities, DirectoryStrings.ExArgumentOutOfRangeException("sourceCapabilities", sourceCapabilities));
			}
			foreach (Capability item in from c in CapabilityHelper.AllowedSKUCapabilities
			where targetCapabilities.Contains(c)
			select c)
			{
				targetCapabilities.Remove(item);
			}
			foreach (Capability item2 in sourceCapabilities)
			{
				targetCapabilities.Add(item2);
			}
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x0002CF80 File Offset: 0x0002B180
		internal static bool HasBposSKUCapability(MultiValuedProperty<Capability> capabilities)
		{
			return CapabilityHelper.GetSKUCapability(capabilities) != null;
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x0002CF9B File Offset: 0x0002B19B
		internal static bool AllowMailboxLogon(Capability? skuCapability, bool? skuAssigned, DateTime? whenMailboxCreated)
		{
			if (skuCapability != null && CapabilityHelper.IsFreeSkuCapability(skuCapability.Value))
			{
				return true;
			}
			if (skuAssigned != null)
			{
				return skuAssigned.Value;
			}
			return CapabilityHelper.IsWithinGracePeriod(whenMailboxCreated);
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x0002CFD0 File Offset: 0x0002B1D0
		private static List<Capability> GetRootSKUCapabilities(MultiValuedProperty<Capability> capabilities)
		{
			List<Capability> list = new List<Capability>();
			foreach (Capability item in capabilities)
			{
				if (CapabilityHelper.RootSKUCapabilities.Contains(item))
				{
					list.Add(item);
				}
			}
			return list;
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x060009F2 RID: 2546 RVA: 0x0002D034 File Offset: 0x0002B234
		internal static Capability[] AllowedSKUCapabilities
		{
			get
			{
				if (CapabilityHelper.allowedSKUCapabilities == null)
				{
					CapabilityHelper.allowedSKUCapabilities = CapabilityHelper.AllowedSKUCapabilitiesList.ToArray();
				}
				return CapabilityHelper.allowedSKUCapabilities;
			}
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x0002D051 File Offset: 0x0002B251
		internal static bool IsAllowedSKUCapability(Capability capability)
		{
			return CapabilityHelper.AllowedSKUCapabilitiesList.Contains(capability);
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x0002D05E File Offset: 0x0002B25E
		internal static bool IsRootSKUCapability(Capability capability)
		{
			return CapabilityHelper.RootSKUCapabilities.Contains(capability);
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x0002D06B File Offset: 0x0002B26B
		internal static bool IsAddOnSKUCapability(Capability capability)
		{
			return CapabilityHelper.AddOnSKUCapabilities.Contains(capability);
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x0002D078 File Offset: 0x0002B278
		internal static Guid GetSKUCapabilityGuid(Capability capability)
		{
			if (CapabilityHelper.SkuCapabilityGuidMap.ContainsKey(capability))
			{
				return CapabilityHelper.SkuCapabilityGuidMap[capability];
			}
			return Guid.Empty;
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x0002D098 File Offset: 0x0002B298
		internal static bool GetIsLicensingEnforcedInOrg(OrganizationId organizationId)
		{
			OrganizationProperties organizationProperties;
			return !OrganizationPropertyCache.TryGetOrganizationProperties(organizationId, out organizationProperties) || organizationProperties.IsLicensingEnforced;
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x0002D0B7 File Offset: 0x0002B2B7
		private static bool IsFreeSkuCapability(Capability skuCapability)
		{
			return CapabilityHelper.FreeSKUCapabilities.Contains(skuCapability);
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x0002D0C4 File Offset: 0x0002B2C4
		private static bool IsWithinGracePeriod(DateTime? whenMailboxCreated)
		{
			return whenMailboxCreated != null && DateTime.UtcNow < whenMailboxCreated.Value + CapabilityHelper.gracePeriod;
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x060009FA RID: 2554 RVA: 0x0002D0EF File Offset: 0x0002B2EF
		// (set) Token: 0x060009FB RID: 2555 RVA: 0x0002D0F6 File Offset: 0x0002B2F6
		private static IDictionary<Capability, Guid> SkuCapabilityGuidMap { get; set; } = new Dictionary<Capability, Guid>();

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x060009FC RID: 2556 RVA: 0x0002D0FE File Offset: 0x0002B2FE
		// (set) Token: 0x060009FD RID: 2557 RVA: 0x0002D105 File Offset: 0x0002B305
		private static List<Capability> AllowedSKUCapabilitiesList { get; set; } = new List<Capability>();

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x060009FE RID: 2558 RVA: 0x0002D10D File Offset: 0x0002B30D
		// (set) Token: 0x060009FF RID: 2559 RVA: 0x0002D114 File Offset: 0x0002B314
		private static List<Capability> RootSKUCapabilities { get; set; } = new List<Capability>();

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000A00 RID: 2560 RVA: 0x0002D11C File Offset: 0x0002B31C
		// (set) Token: 0x06000A01 RID: 2561 RVA: 0x0002D123 File Offset: 0x0002B323
		private static List<Capability> AddOnSKUCapabilities { get; set; } = new List<Capability>();

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000A02 RID: 2562 RVA: 0x0002D12B File Offset: 0x0002B32B
		// (set) Token: 0x06000A03 RID: 2563 RVA: 0x0002D132 File Offset: 0x0002B332
		private static List<Capability> FreeSKUCapabilities { get; set; } = new List<Capability>();

		// Token: 0x06000A04 RID: 2564 RVA: 0x0002D13C File Offset: 0x0002B33C
		static CapabilityHelper()
		{
			foreach (FieldInfo fieldInfo in typeof(Capability).GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.GetField))
			{
				SKUCapabilityAttribute[] array = (SKUCapabilityAttribute[])fieldInfo.GetCustomAttributes(typeof(SKUCapabilityAttribute), false);
				if (array != null && array.Length > 0)
				{
					Capability capability = (Capability)fieldInfo.GetValue(null);
					CapabilityHelper.SkuCapabilityGuidMap[capability] = array[0].Guid;
					CapabilityHelper.AllowedSKUCapabilitiesList.Add(capability);
					if (array[0].AddOnSKU)
					{
						CapabilityHelper.AddOnSKUCapabilities.Add(capability);
					}
					else
					{
						CapabilityHelper.RootSKUCapabilities.Add(capability);
					}
					if (array[0].Free)
					{
						CapabilityHelper.FreeSKUCapabilities.Add(capability);
					}
				}
			}
		}

		// Token: 0x040003BE RID: 958
		internal const string ParameterSKUCapability = "SKUCapability";

		// Token: 0x040003BF RID: 959
		internal const string ParameterAddOnSKUCapability = "AddOnSKUCapability";

		// Token: 0x040003C0 RID: 960
		internal const string ParameterTenantSKUCapability = "TenantSKUCapability";

		// Token: 0x040003C1 RID: 961
		internal static readonly string[] ffoOffers = new string[]
		{
			"EXCHANGE ONLINE PROTECTION ENTERPRISE SUBSCRIPTION"
		};

		// Token: 0x040003C2 RID: 962
		internal static readonly string[] ffoPremiumOffers = new string[]
		{
			"EXCHANGE ONLINE PROTECTION ENTERPRISE PREMIUM SUBSCRIPTION"
		};

		// Token: 0x040003C3 RID: 963
		private static Capability[] allowedSKUCapabilities;

		// Token: 0x040003C4 RID: 964
		private static readonly TimeSpan gracePeriod = TimeSpan.FromDays(30.0);
	}
}
