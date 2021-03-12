using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Edm;

namespace Microsoft.Exchange.Services.OData.Web
{
	// Token: 0x02000E04 RID: 3588
	internal static class ODataPathExtensions
	{
		// Token: 0x06005CF5 RID: 23797 RVA: 0x00122300 File Offset: 0x00120500
		public static string GetIdKey(this ODataPathSegment segment)
		{
			ArgumentValidator.ThrowIfNull("segment", segment);
			ArgumentValidator.ThrowIfTypeInvalid<KeySegment>("segment", segment);
			KeySegment keySegment = (KeySegment)segment;
			string key = keySegment.Keys.Single((KeyValuePair<string, object> x) => x.Key.Equals("Id")).Value.ToString();
			return UrlUtilities.TrimKey(key);
		}

		// Token: 0x06005CF6 RID: 23798 RVA: 0x00122368 File Offset: 0x00120568
		public static string GetSingletonName(this ODataPathSegment segment)
		{
			ArgumentValidator.ThrowIfNull("segment", segment);
			ArgumentValidator.ThrowIfTypeInvalid<SingletonSegment>("segment", segment);
			SingletonSegment singletonSegment = (SingletonSegment)segment;
			return singletonSegment.Singleton.Name;
		}

		// Token: 0x06005CF7 RID: 23799 RVA: 0x001223A0 File Offset: 0x001205A0
		public static string GetPropertyName(this ODataPathSegment segment)
		{
			ArgumentValidator.ThrowIfNull("segment", segment);
			ArgumentValidator.ThrowIfTypeInvalid<NavigationPropertySegment>("segment", segment);
			NavigationPropertySegment navigationPropertySegment = (NavigationPropertySegment)segment;
			return navigationPropertySegment.NavigationProperty.Name;
		}

		// Token: 0x06005CF8 RID: 23800 RVA: 0x001223D8 File Offset: 0x001205D8
		public static string GetEntitySetName(this ODataPathSegment segment)
		{
			ArgumentValidator.ThrowIfNull("segment", segment);
			ArgumentValidator.ThrowIfTypeInvalid<EntitySetSegment>("segment", segment);
			EntitySetSegment entitySetSegment = (EntitySetSegment)segment;
			return entitySetSegment.EntitySet.Name;
		}

		// Token: 0x06005CF9 RID: 23801 RVA: 0x0012240D File Offset: 0x0012060D
		public static string GetEntitySetOrPropertyName(this ODataPathSegment segment)
		{
			ArgumentValidator.ThrowIfNull("segment", segment);
			if (segment is EntitySetSegment)
			{
				return segment.GetEntitySetName();
			}
			return segment.GetPropertyName();
		}

		// Token: 0x06005CFA RID: 23802 RVA: 0x00122430 File Offset: 0x00120630
		public static string GetActionName(this ODataPathSegment segment)
		{
			ArgumentValidator.ThrowIfNull("segment", segment);
			ArgumentValidator.ThrowIfTypeInvalid<OperationSegment>("segment", segment);
			OperationSegment operationSegment = (OperationSegment)segment;
			return operationSegment.Operations.FirstOrDefault<IEdmOperation>().Name;
		}
	}
}
