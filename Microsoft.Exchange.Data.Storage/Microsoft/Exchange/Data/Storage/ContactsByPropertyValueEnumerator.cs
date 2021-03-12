using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020004CC RID: 1228
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class ContactsByPropertyValueEnumerator : IEnumerable<IStorePropertyBag>, IEnumerable
	{
		// Token: 0x060035E0 RID: 13792 RVA: 0x000D9354 File Offset: 0x000D7554
		public ContactsByPropertyValueEnumerator(IFolder folder, PropertyDefinition property, IEnumerable<string> propertyValues, PropertyDefinition[] requestedProperties)
		{
			ArgumentValidator.ThrowIfNull("folder", folder);
			ArgumentValidator.ThrowIfNull("property", property);
			ArgumentValidator.ThrowIfNull("propertyValues", propertyValues);
			ArgumentValidator.ThrowIfNull("requestedProperties", requestedProperties);
			this.folder = folder;
			this.property = property;
			this.propertyValues = propertyValues;
			this.requestedProperties = requestedProperties;
		}

		// Token: 0x060035E1 RID: 13793 RVA: 0x000D97DC File Offset: 0x000D79DC
		public IEnumerator<IStorePropertyBag> GetEnumerator()
		{
			PropertyDefinition[] additionalProperties = new PropertyDefinition[]
			{
				this.property
			};
			PropertyDefinition[] allProperties = PropertyDefinitionCollection.Merge<PropertyDefinition>(additionalProperties, this.requestedProperties);
			SortBy[] ascendingSortBy = new SortBy[]
			{
				new SortBy(this.property, SortOrder.Ascending)
			};
			using (IQueryResult queryResult = this.folder.IItemQuery(ItemQueryType.None, null, ascendingSortBy, allProperties))
			{
				foreach (string propertyValue in this.propertyValues)
				{
					ContactsByPropertyValueEnumerator.Tracer.TraceDebug<string, PropertyDefinition>((long)this.GetHashCode(), "ContactsByPropertyValueEnumerator: Finding matches for {0} in property {1}.", propertyValue, this.property);
					QueryFilter filter = new TextFilter(this.property, propertyValue, MatchOptions.FullString, MatchFlags.IgnoreCase);
					if (!queryResult.SeekToCondition(SeekReference.OriginBeginning, filter, SeekToConditionFlags.AllowExtendedFilters))
					{
						ContactsByPropertyValueEnumerator.Tracer.TraceDebug<string>((long)this.GetHashCode(), "ContactsByPropertyValueEnumerator: no matching contacts found.", propertyValue);
					}
					else
					{
						bool more = true;
						while (more)
						{
							IStorePropertyBag[] contacts = queryResult.GetPropertyBags(10);
							if (contacts == null || contacts.Length == 0)
							{
								ContactsByPropertyValueEnumerator.Tracer.TraceDebug((long)this.GetHashCode(), "ContactsByPropertyValueEnumerator: no more rows to fecth.");
								break;
							}
							foreach (IStorePropertyBag contact in contacts)
							{
								string retrievedPropertyValue = contact.GetValueOrDefault<string>(this.property, null);
								if (string.IsNullOrEmpty(retrievedPropertyValue))
								{
									ContactsByPropertyValueEnumerator.Tracer.TraceDebug((long)this.GetHashCode(), "ContactsByPropertyValueEnumerator: stop looking for matches, found a null/empty property value.");
									more = false;
									break;
								}
								if (!StringComparer.OrdinalIgnoreCase.Equals(retrievedPropertyValue, propertyValue))
								{
									ContactsByPropertyValueEnumerator.Tracer.TraceDebug((long)this.GetHashCode(), "ContactsByPropertyValueEnumerator: stop looking for matches, found a mismatch.");
									more = false;
									break;
								}
								ContactsByPropertyValueEnumerator.Tracer.TraceDebug((long)this.GetHashCode(), "ContactsByPropertyValueEnumerator: Found a match, returning the contact.");
								yield return contact;
							}
						}
					}
				}
			}
			yield break;
		}

		// Token: 0x060035E2 RID: 13794 RVA: 0x000D97F8 File Offset: 0x000D79F8
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotSupportedException("Must use the generics interface of GetEnumerator.");
		}

		// Token: 0x04001CEA RID: 7402
		private const int ChunkSize = 10;

		// Token: 0x04001CEB RID: 7403
		private static readonly Trace Tracer = ExTraceGlobals.ContactsEnumeratorTracer;

		// Token: 0x04001CEC RID: 7404
		private readonly IFolder folder;

		// Token: 0x04001CED RID: 7405
		private readonly PropertyDefinition property;

		// Token: 0x04001CEE RID: 7406
		private readonly IEnumerable<string> propertyValues;

		// Token: 0x04001CEF RID: 7407
		private readonly PropertyDefinition[] requestedProperties;
	}
}
