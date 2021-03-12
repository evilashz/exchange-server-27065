using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Search;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x02000150 RID: 336
	internal sealed class SearchParametersProperty : ComplexPropertyBase, IToXmlCommand, IToServiceObjectCommand, ISetCommand, ISetUpdateCommand, IUpdateCommand, IPropertyCommand
	{
		// Token: 0x06000935 RID: 2357 RVA: 0x0002CF4C File Offset: 0x0002B14C
		public SearchParametersProperty(CommandContext commandContext) : base(commandContext)
		{
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x0002CF58 File Offset: 0x0002B158
		private void SetSearchParameters(SearchParametersType searchParameters, SearchFolder folder)
		{
			IdConverter idConverter = this.commandContext.IdConverter;
			List<StoreId> list = new List<StoreId>();
			foreach (BaseFolderId folderId in searchParameters.BaseFolderIds)
			{
				IdAndSession idAndSession = idConverter.ConvertFolderIdToIdAndSession(folderId, IdConverter.ConvertOption.IgnoreChangeKey | IdConverter.ConvertOption.NoBind);
				list.Add(idAndSession.Id);
			}
			RestrictionType restriction = searchParameters.Restriction;
			QueryFilter searchQuery = null;
			if (restriction != null && restriction.Item != null)
			{
				ServiceObjectToFilterConverter serviceObjectToFilterConverter = new ServiceObjectToFilterConverter();
				searchQuery = serviceObjectToFilterConverter.Convert(restriction.Item);
			}
			bool deepTraversal = searchParameters.Traversal == SearchFolderTraversalType.Deep;
			folder.ApplyContinuousSearch(new SearchFolderCriteria(searchQuery, list.ToArray())
			{
				DeepTraversal = deepTraversal
			});
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x0002D008 File Offset: 0x0002B208
		private SearchParametersType CreateSearchParameters(SearchFolder folder, ToServiceObjectCommandSettings commandSettings)
		{
			SearchParametersType searchParametersType = new SearchParametersType();
			SearchFolderCriteria searchCriteria = folder.GetSearchCriteria();
			searchParametersType.Traversal = (searchCriteria.DeepTraversal ? SearchFolderTraversalType.Deep : SearchFolderTraversalType.Shallow);
			searchParametersType.TraversalSpecified = true;
			if (searchCriteria.SearchQuery != null)
			{
				QueryFilterToSearchExpressionConverter queryFilterToSearchExpressionConverter = new QueryFilterToSearchExpressionConverter();
				searchParametersType.Restriction = new RestrictionType
				{
					Item = queryFilterToSearchExpressionConverter.Convert(searchCriteria.SearchQuery)
				};
			}
			List<BaseFolderId> list = new List<BaseFolderId>();
			foreach (StoreId storeId in searchCriteria.FolderScope)
			{
				ConcatenatedIdAndChangeKey concatenatedId = IdConverter.GetConcatenatedId(storeId, commandSettings.IdAndSession, null);
				list.Add(new FolderId
				{
					Id = concatenatedId.Id,
					ChangeKey = concatenatedId.ChangeKey
				});
			}
			searchParametersType.BaseFolderIds = list.ToArray();
			return searchParametersType;
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x0002D0DB File Offset: 0x0002B2DB
		public static SearchParametersProperty CreateCommand(CommandContext commandContext)
		{
			return new SearchParametersProperty(commandContext);
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x0002D0E4 File Offset: 0x0002B2E4
		public void ToServiceObject()
		{
			ToServiceObjectCommandSettings commandSettings = base.GetCommandSettings<ToServiceObjectCommandSettings>();
			StoreObject storeObject = commandSettings.StoreObject;
			ServiceObject serviceObject = commandSettings.ServiceObject;
			PropertyInformation propertyInformation = this.commandContext.PropertyInformation;
			SearchFolder searchFolder = storeObject as SearchFolder;
			try
			{
				serviceObject[propertyInformation] = this.CreateSearchParameters(searchFolder, commandSettings);
			}
			catch (UnsupportedTypeForConversionException)
			{
				ExTraceGlobals.CommonAlgorithmTracer.TraceError<string>((long)this.GetHashCode(), "SearchParameterProperty.ToServiceObject encountered UnsupportedTypeForConversionException for searchFolder: {0}", searchFolder.DisplayName);
			}
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x0002D15C File Offset: 0x0002B35C
		void ISetCommand.Set()
		{
			SetCommandSettings commandSettings = base.GetCommandSettings<SetCommandSettings>();
			StoreObject storeObject = commandSettings.StoreObject;
			ServiceObject serviceObject = commandSettings.ServiceObject;
			SearchFolder folder = storeObject as SearchFolder;
			PropertyInformation propertyInformation = this.commandContext.PropertyInformation;
			SearchParametersType valueOrDefault = serviceObject.GetValueOrDefault<SearchParametersType>(propertyInformation);
			this.SetSearchParameters(valueOrDefault, folder);
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x0002D1A5 File Offset: 0x0002B3A5
		void ISetUpdateCommand.SetUpdate(SetPropertyUpdate setPropertyUpdate, UpdateCommandSettings updateCommandSettings)
		{
			this.SetUpdate(setPropertyUpdate, updateCommandSettings);
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x0002D1B0 File Offset: 0x0002B3B0
		public override void SetUpdate(SetPropertyUpdate setPropertyUpdate, UpdateCommandSettings updateCommandSettings)
		{
			SearchFolder folder = updateCommandSettings.StoreObject as SearchFolder;
			ServiceObject serviceObject = setPropertyUpdate.ServiceObject;
			PropertyInformation propertyInformation = this.commandContext.PropertyInformation;
			SearchParametersType valueOrDefault = serviceObject.GetValueOrDefault<SearchParametersType>(propertyInformation);
			this.SetSearchParameters(valueOrDefault, folder);
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x0002D1EC File Offset: 0x0002B3EC
		void IToXmlCommand.ToXml()
		{
			throw new InvalidOperationException("SearchParametersProperty.ToXml should not be called.");
		}
	}
}
