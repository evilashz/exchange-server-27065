using System;
using System.Collections.Generic;
using System.Globalization;
using System.Management.Automation;
using System.Reflection;
using System.ServiceModel;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.Transport;
using Microsoft.Exchange.Data.QueueDigest;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Net.DiagnosticsAggregation;

namespace Microsoft.Exchange.Management.QueueDigest
{
	// Token: 0x0200006A RID: 106
	internal class GetQueueDigestMtrtImpl : GetQueueDigestImpl
	{
		// Token: 0x060003C0 RID: 960 RVA: 0x0000E13F File Offset: 0x0000C33F
		public GetQueueDigestMtrtImpl(GetQueueDigest cmdlet)
		{
			this.cmdlet = cmdlet;
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0000E16F File Offset: 0x0000C36F
		public override void ResolveForForest()
		{
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0000E171 File Offset: 0x0000C371
		public override void ResolveDag(DatabaseAvailabilityGroup dag)
		{
			this.resolvedDags.Add(dag.Id);
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0000E185 File Offset: 0x0000C385
		public override void ResolveAdSite(ADSite adSite)
		{
			this.resolvedAdSites.Add(adSite.Id);
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0000E199 File Offset: 0x0000C399
		public override void ResolveServer(Server server)
		{
			this.resolvedServers.Add(server.Id);
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0000E1B0 File Offset: 0x0000C3B0
		public override void ProcessRecord()
		{
			MultiValuedProperty<ComparisonFilter> dataFilter = this.SplitQueryFilter(this.cmdlet.QueryFilter);
			this.cmdlet.WriteDebug("Connecting to MTRT");
			MultiValuedProperty<Guid> siteIds;
			MultiValuedProperty<Guid> dagIds;
			MultiValuedProperty<Guid> serverIds;
			this.GetParameterIdentities(out siteIds, out dagIds, out serverIds);
			Guid guid;
			string text;
			TransportADUtils.GetForestInformation(out guid, out text);
			this.cmdlet.WriteDebug(string.Format(CultureInfo.InvariantCulture, "ForestName: {0}; ForestGuid: {1};", new object[]
			{
				text,
				guid
			}));
			Exception ex = null;
			try
			{
				int num = 0;
				foreach (TransportQueueStatistics mtrtQueueAggregate in this.FindQueueFromMtrt(guid, dataFilter, siteIds, dagIds, serverIds))
				{
					this.cmdlet.WriteObject(QueueDigestPresentationObject.Create(mtrtQueueAggregate, this.cmdlet.GroupBy));
					num++;
					if (!this.cmdlet.ResultSize.IsUnlimited && (long)num == (long)((ulong)this.cmdlet.ResultSize.Value))
					{
						break;
					}
				}
			}
			catch (FaultException<DiagnosticsAggregationFault> faultException)
			{
				ex = faultException;
			}
			catch (CommunicationException ex2)
			{
				ex = ex2;
			}
			catch (TimeoutException ex3)
			{
				ex = ex3;
			}
			catch (Exception ex4)
			{
				this.cmdlet.WriteDebug("Unhandled Excpetion: " + ex4.ToString());
				throw;
			}
			if (ex != null)
			{
				this.cmdlet.WriteError(new LocalizedException(Strings.GetQueueDigestFromMtrtFailed(ex.ToString())), ErrorCategory.ReadError, null);
			}
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0000E358 File Offset: 0x0000C558
		private IEnumerable<TransportQueueStatistics> FindQueueFromMtrt(Guid forestGuid, MultiValuedProperty<ComparisonFilter> dataFilter, MultiValuedProperty<Guid> siteIds, MultiValuedProperty<Guid> dagIds, MultiValuedProperty<Guid> serverIds)
		{
			Assembly assembly = Assembly.Load("Microsoft.Exchange.Hygiene.Data");
			Type type = assembly.GetType("Microsoft.Exchange.Hygiene.Data.MessageTrace.TransportQueueSession");
			Type typeFromHandle = typeof(MultiValuedProperty<Guid>);
			Type typeFromHandle2 = typeof(MultiValuedProperty<ComparisonFilter>);
			MethodInfo method = type.GetMethod("FindTransportQueueInfo", BindingFlags.Instance | BindingFlags.Public, null, new Type[]
			{
				typeof(Guid),
				typeof(string),
				typeof(TimeSpan),
				typeFromHandle,
				typeFromHandle,
				typeFromHandle,
				typeFromHandle2,
				typeof(DetailsLevel),
				typeof(int),
				typeof(int)
			}, null);
			object obj = Activator.CreateInstance(type);
			IEnumerable<TransportQueueStatistics> result;
			try
			{
				int num = 1000;
				if (!this.cmdlet.ResultSize.IsUnlimited)
				{
					num = (int)Math.Min(this.cmdlet.ResultSize.Value, 1000U);
				}
				TimeSpan timeSpan = TimeSpan.FromHours(2.0);
				object obj2 = method.Invoke(obj, new object[]
				{
					forestGuid,
					this.cmdlet.GroupBy.ToString(),
					timeSpan,
					siteIds,
					dagIds,
					serverIds,
					dataFilter,
					this.cmdlet.DetailsLevel,
					num,
					100
				});
				result = (obj2 as IEnumerable<TransportQueueStatistics>);
			}
			catch (TargetInvocationException ex)
			{
				this.cmdlet.WriteError(new LocalizedException(Strings.GetQueueDigestUnexpectedError(ex.ToString())), ErrorCategory.ReadError, null);
				throw;
			}
			return result;
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0000E534 File Offset: 0x0000C734
		private static MultiValuedProperty<Guid> GetObjectGuids(HashSet<ADObjectId> objectIds)
		{
			MultiValuedProperty<Guid> multiValuedProperty = new MultiValuedProperty<Guid>();
			foreach (ADObjectId adobjectId in objectIds)
			{
				multiValuedProperty.Add(adobjectId.ObjectGuid);
			}
			return multiValuedProperty;
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0000E590 File Offset: 0x0000C790
		private void GetParameterIdentities(out MultiValuedProperty<Guid> siteIds, out MultiValuedProperty<Guid> dagIds, out MultiValuedProperty<Guid> serverIds)
		{
			siteIds = null;
			dagIds = null;
			serverIds = null;
			if (this.cmdlet.ParameterSetName == "ServerParameterSet")
			{
				serverIds = GetQueueDigestMtrtImpl.GetObjectGuids(this.resolvedServers);
				this.LogDebug(serverIds);
				return;
			}
			if (this.cmdlet.ParameterSetName == "DagParameterSet")
			{
				dagIds = GetQueueDigestMtrtImpl.GetObjectGuids(this.resolvedDags);
				this.LogDebug(dagIds);
				return;
			}
			if (this.cmdlet.ParameterSetName == "SiteParameterSet")
			{
				siteIds = GetQueueDigestMtrtImpl.GetObjectGuids(this.resolvedAdSites);
				this.LogDebug(siteIds);
			}
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0000E62C File Offset: 0x0000C82C
		private MultiValuedProperty<ComparisonFilter> SplitQueryFilter(QueryFilter filter)
		{
			MultiValuedProperty<ComparisonFilter> multiValuedProperty = new MultiValuedProperty<ComparisonFilter>();
			if (filter == null)
			{
				return multiValuedProperty;
			}
			ComparisonFilter comparisonFilter = filter as ComparisonFilter;
			AndFilter andFilter = filter as AndFilter;
			if (comparisonFilter != null)
			{
				multiValuedProperty.Add(comparisonFilter);
			}
			else if (andFilter != null)
			{
				Stack<AndFilter> stack = new Stack<AndFilter>();
				stack.Push(andFilter);
				while (stack.Count != 0)
				{
					AndFilter andFilter2 = stack.Pop();
					foreach (QueryFilter queryFilter in andFilter2.Filters)
					{
						ComparisonFilter comparisonFilter2 = queryFilter as ComparisonFilter;
						AndFilter andFilter3 = queryFilter as AndFilter;
						if (comparisonFilter2 != null)
						{
							multiValuedProperty.Add(comparisonFilter2);
						}
						else if (andFilter3 != null)
						{
							stack.Push(andFilter3);
						}
						else
						{
							this.cmdlet.WriteError(new LocalizedException(Strings.GetQueueDigestInvalidFilter(queryFilter.ToString())), ErrorCategory.InvalidArgument, null);
						}
					}
				}
			}
			else
			{
				this.cmdlet.WriteError(new LocalizedException(Strings.GetQueueDigestInvalidFilter(filter.ToString())), ErrorCategory.InvalidArgument, null);
			}
			return multiValuedProperty;
		}

		// Token: 0x060003CA RID: 970 RVA: 0x0000E73C File Offset: 0x0000C93C
		private void LogDebug(IEnumerable<Guid> identities)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (Guid guid in identities)
			{
				if (stringBuilder.Length == 0)
				{
					stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0}:", new object[]
					{
						this.cmdlet.ParameterSetName
					});
				}
				else
				{
					stringBuilder.AppendFormat(CultureInfo.InvariantCulture, ", ", new object[0]);
				}
				stringBuilder.Append(guid);
			}
			this.cmdlet.WriteDebug(stringBuilder.ToString());
		}

		// Token: 0x0400015A RID: 346
		private GetQueueDigest cmdlet;

		// Token: 0x0400015B RID: 347
		private HashSet<ADObjectId> resolvedAdSites = new HashSet<ADObjectId>();

		// Token: 0x0400015C RID: 348
		private HashSet<ADObjectId> resolvedDags = new HashSet<ADObjectId>();

		// Token: 0x0400015D RID: 349
		private HashSet<ADObjectId> resolvedServers = new HashSet<ADObjectId>();
	}
}
