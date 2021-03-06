using System;
using System.Collections;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020000EB RID: 235
	internal sealed class ClientMappingComparer : IComparer
	{
		// Token: 0x060007E1 RID: 2017 RVA: 0x0003B1AF File Offset: 0x000393AF
		internal ClientMappingComparer(Hashtable registries)
		{
			this.registries = registries;
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x0003B1C0 File Offset: 0x000393C0
		int IComparer.Compare(object x, object y)
		{
			ClientMapping clientMapping = x as ClientMapping;
			ClientMapping clientMapping2 = y as ClientMapping;
			ExTraceGlobals.FormsRegistryDataTracer.TraceDebug<ClientMapping, ClientMapping>((long)this.GetHashCode(), "ClientMappingComparer::Compare. a = {0}, b = {1}", clientMapping, clientMapping2);
			if (clientMapping == null && clientMapping2 == null)
			{
				return 0;
			}
			if (clientMapping == null)
			{
				return -1;
			}
			if (clientMapping2 == null)
			{
				return 1;
			}
			int num = string.Compare(clientMapping.Application, clientMapping2.Application, StringComparison.Ordinal);
			if (num != 0)
			{
				return num;
			}
			num = string.Compare(clientMapping.Platform, clientMapping2.Platform, StringComparison.Ordinal);
			if (num != 0)
			{
				return num;
			}
			num = clientMapping.Control - clientMapping2.Control;
			if (num != 0)
			{
				return num;
			}
			double num2 = (double)clientMapping.MinimumVersion.CompareTo(clientMapping2.MinimumVersion);
			if (num2 != 0.0)
			{
				if (num2 <= 0.0)
				{
					return -1;
				}
				return 1;
			}
			else
			{
				FormsRegistry formsRegistry = clientMapping.Experience.FormsRegistry;
				FormsRegistry formsRegistry2 = clientMapping2.Experience.FormsRegistry;
				if (this.IsParentRegistry(formsRegistry, formsRegistry2))
				{
					return 1;
				}
				if (this.IsParentRegistry(formsRegistry2, formsRegistry))
				{
					return -1;
				}
				return 0;
			}
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x0003B2C0 File Offset: 0x000394C0
		private bool IsParentRegistry(FormsRegistry registry, FormsRegistry parentCandidate)
		{
			FormsRegistry formsRegistry = (FormsRegistry)this.registries[registry.InheritsFrom];
			while (formsRegistry != null)
			{
				if (string.Equals(formsRegistry.Name, parentCandidate.Name, StringComparison.Ordinal))
				{
					return true;
				}
				if (formsRegistry.InheritsFrom.Length != 0)
				{
					formsRegistry = (FormsRegistry)this.registries[formsRegistry.InheritsFrom];
				}
				else
				{
					formsRegistry = null;
				}
			}
			return false;
		}

		// Token: 0x04000599 RID: 1433
		private Hashtable registries;
	}
}
