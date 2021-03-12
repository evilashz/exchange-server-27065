using System;
using System.Collections;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.HttpProxy.Routing;

namespace Microsoft.Exchange.HttpProxy.AddressFinder
{
	// Token: 0x02000006 RID: 6
	internal static class AddressFinderHelper
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002422 File Offset: 0x00000622
		public static IRoutingKey[] EmptyRoutingKeyArray
		{
			get
			{
				return AddressFinderHelper.StaticEmptyRoutingKeyArray;
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002429 File Offset: 0x00000629
		public static IRoutingKey[] GetRoutingKeyArray(params IRoutingKey[] routingKeys)
		{
			return routingKeys;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000242C File Offset: 0x0000062C
		public static bool IsNullOrEmpty(this ICollection collection)
		{
			return collection == null || collection.Count == 0;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000243C File Offset: 0x0000063C
		public static void ThrowIfNull(AddressFinderSource source, IAddressFinderDiagnostics diagnostics)
		{
			ArgumentValidator.ThrowIfNull("source", source);
			ArgumentValidator.ThrowIfNull("diagnostics", diagnostics);
		}

		// Token: 0x0400000F RID: 15
		private static readonly IRoutingKey[] StaticEmptyRoutingKeyArray = new IRoutingKey[0];
	}
}
