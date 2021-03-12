using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020001FB RID: 507
	public class OwaResponseShapeResolver : IResponseShapeResolver
	{
		// Token: 0x060011DD RID: 4573 RVA: 0x00044F4C File Offset: 0x0004314C
		public OwaResponseShapeResolver()
		{
			this.responseShapesMap = WellKnownShapes.ResponseShapes;
		}

		// Token: 0x060011DE RID: 4574 RVA: 0x00044F60 File Offset: 0x00043160
		public T GetResponseShape<T>(string shapeName, T clientResponseShape, IFeaturesManager featuresManager = null) where T : ResponseShape
		{
			if (string.IsNullOrEmpty(shapeName))
			{
				return clientResponseShape;
			}
			WellKnownShapeName key;
			ResponseShape responseShape;
			if (!Enum.TryParse<WellKnownShapeName>(shapeName, out key) || !this.responseShapesMap.TryGetValue(key, out responseShape))
			{
				return clientResponseShape;
			}
			if (clientResponseShape == null)
			{
				return (T)((object)responseShape);
			}
			OwaResponseShapeResolver.AddFlightedProperties(clientResponseShape, responseShape, featuresManager);
			return this.MergeResponseShapes<T>(clientResponseShape, (T)((object)responseShape));
		}

		// Token: 0x060011DF RID: 4575 RVA: 0x00044FBC File Offset: 0x000431BC
		private static void AddFlightedProperties(ResponseShape clientShape, ResponseShape namedShape, IFeaturesManager featuresManager)
		{
			if (namedShape.FlightedProperties == null || featuresManager == null)
			{
				return;
			}
			HashSet<PropertyPath> hashSet = new HashSet<PropertyPath>();
			foreach (string text in namedShape.FlightedProperties.Keys)
			{
				if (featuresManager.IsFeatureSupported(text))
				{
					hashSet.UnionWith(namedShape.FlightedProperties[text]);
				}
			}
			if (hashSet.Any<PropertyPath>())
			{
				if (clientShape.AdditionalProperties != null)
				{
					hashSet.UnionWith(clientShape.AdditionalProperties);
				}
				clientShape.AdditionalProperties = hashSet.ToArray<PropertyPath>();
			}
		}

		// Token: 0x060011E0 RID: 4576 RVA: 0x00045064 File Offset: 0x00043264
		private T MergeResponseShapes<T>(T clientShape, T namedShape) where T : ResponseShape
		{
			PropertyPath[] additionalProperties = clientShape.AdditionalProperties;
			PropertyPath[] additionalProperties2 = namedShape.AdditionalProperties;
			if (additionalProperties == null || additionalProperties.Length == 0)
			{
				clientShape.AdditionalProperties = additionalProperties2;
			}
			else if (additionalProperties2 != null && additionalProperties2.Length > 0)
			{
				List<PropertyPath> list = new List<PropertyPath>(additionalProperties2.Length + additionalProperties.Length);
				list.AddRange(additionalProperties2);
				list.AddRange(additionalProperties);
				clientShape.AdditionalProperties = list.ToArray();
			}
			return clientShape;
		}

		// Token: 0x04000A8C RID: 2700
		private readonly Dictionary<WellKnownShapeName, ResponseShape> responseShapesMap;
	}
}
