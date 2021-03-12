using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Sync;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000111 RID: 273
	[Serializable]
	public sealed class FailedMSOSyncObjectIdParameter : CompoundSyncObjectIdParameter
	{
		// Token: 0x060009D2 RID: 2514 RVA: 0x0002136F File Offset: 0x0001F56F
		public FailedMSOSyncObjectIdParameter(string identity) : base(identity)
		{
			if (!base.IsServiceInstanceDefinied && base.ServiceInstanceIdentity != "*")
			{
				throw new ArgumentException(Strings.ErrorInvalidParameterFormat("identity"), "identity");
			}
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x000213AC File Offset: 0x0001F5AC
		public FailedMSOSyncObjectIdParameter(CompoundSyncObjectId compoundSyncObjectId) : base(compoundSyncObjectId)
		{
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x000213B5 File Offset: 0x0001F5B5
		public FailedMSOSyncObjectIdParameter()
		{
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x000213C0 File Offset: 0x0001F5C0
		public override IEnumerable<T> GetObjects<T>(ObjectId rootId, IConfigDataProvider session, OptionalIdentityData optionalData, out LocalizedString? notFoundReason)
		{
			notFoundReason = null;
			QueryFilter objectFilter = this.GetObjectFilter();
			return session.FindPaged<T>(objectFilter, rootId, !base.IsServiceInstanceDefinied, null, 0);
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x000213F0 File Offset: 0x0001F5F0
		private static void AddSyncObjectIdFilter(List<QueryFilter> filters, string elementValue, ADPropertyDefinition elementProperty)
		{
			if (!string.IsNullOrEmpty(elementValue) && "*" != elementValue)
			{
				if (elementValue.Contains("*"))
				{
					filters.Add(new TextFilter(elementProperty, elementValue, MatchOptions.WildcardString, MatchFlags.IgnoreCase));
					return;
				}
				object propertyValue = elementValue;
				if (elementProperty.Type == typeof(DirectoryObjectClass))
				{
					propertyValue = Enum.Parse(typeof(DirectoryObjectClass), elementValue, true);
				}
				filters.Add(new ComparisonFilter(ComparisonOperator.Equal, elementProperty, propertyValue));
			}
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x00021468 File Offset: 0x0001F668
		private QueryFilter GetObjectFilter()
		{
			string elementValue;
			string elementValue2;
			string elementValue3;
			base.GetSyncObjectIdElements(out elementValue, out elementValue2, out elementValue3);
			List<QueryFilter> list = new List<QueryFilter>();
			FailedMSOSyncObjectIdParameter.AddSyncObjectIdFilter(list, elementValue, FailedMSOSyncObjectSchema.ContextId);
			FailedMSOSyncObjectIdParameter.AddSyncObjectIdFilter(list, elementValue2, FailedMSOSyncObjectSchema.ObjectId);
			FailedMSOSyncObjectIdParameter.AddSyncObjectIdFilter(list, elementValue3, FailedMSOSyncObjectSchema.ExternalDirectoryObjectClass);
			if (list.Count > 0)
			{
				return new AndFilter(list.ToArray());
			}
			return null;
		}
	}
}
