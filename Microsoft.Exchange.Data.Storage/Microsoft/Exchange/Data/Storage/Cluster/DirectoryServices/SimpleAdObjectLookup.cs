using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.HA.DirectoryServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.Diagnostics.Performance;

namespace Microsoft.Exchange.Data.Storage.Cluster.DirectoryServices
{
	// Token: 0x0200042C RID: 1068
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SimpleAdObjectLookup<TADWrapperObject> : IFindAdObject<TADWrapperObject> where TADWrapperObject : class, IADObjectCommon
	{
		// Token: 0x06002FC6 RID: 12230 RVA: 0x000C3ED4 File Offset: 0x000C20D4
		public static TADWrapperObject FindAdObjectWithQueryStatic(IADToplogyConfigurationSession adSession, QueryFilter queryFilter)
		{
			TADWrapperObject answer = default(TADWrapperObject);
			Exception ex = ADUtils.RunADOperation(delegate()
			{
				TADWrapperObject[] array = adSession.Find<TADWrapperObject>(null, QueryScope.SubTree, queryFilter, null, 2);
				if (array.Length > 1)
				{
					SimpleAdObjectLookup<TADWrapperObject>.Tracer.TraceError<QueryFilter, TADWrapperObject, TADWrapperObject>(0L, "FindAdObjectWithQueryStatic found multiple objects with query '{0}'! The first two are '{1}' and '{2}'.", queryFilter, array[0], array[1]);
					answer = array[(Environment.TickCount & int.MaxValue) % 2];
					return;
				}
				if (array.Length == 0)
				{
					SimpleAdObjectLookup<TADWrapperObject>.Tracer.TraceError<QueryFilter>(0L, "FindAdObjectWithQueryStatic found no objects with query '{0}'!", queryFilter);
					return;
				}
				answer = array[0];
			}, 2);
			if (ex != null)
			{
				SimpleAdObjectLookup<TADWrapperObject>.Tracer.TraceError<Exception>(0L, "FindAdObjectWithQueryStatic got an exception: {0}", ex);
			}
			return answer;
		}

		// Token: 0x06002FC7 RID: 12231 RVA: 0x000C3F2F File Offset: 0x000C212F
		public SimpleAdObjectLookup(IADToplogyConfigurationSession adSession)
		{
			this.AdSession = adSession;
		}

		// Token: 0x17000F27 RID: 3879
		// (get) Token: 0x06002FC8 RID: 12232 RVA: 0x000C3F3E File Offset: 0x000C213E
		private static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.ActiveManagerClientTracer;
			}
		}

		// Token: 0x17000F28 RID: 3880
		// (get) Token: 0x06002FC9 RID: 12233 RVA: 0x000C3F45 File Offset: 0x000C2145
		// (set) Token: 0x06002FCA RID: 12234 RVA: 0x000C3F4D File Offset: 0x000C214D
		public IADToplogyConfigurationSession AdSession { get; set; }

		// Token: 0x06002FCB RID: 12235 RVA: 0x000C3F56 File Offset: 0x000C2156
		public static TADWrapperObject FindAdObjectTypeByGuidStatic(IADToplogyConfigurationSession adSession, Guid objectGuid)
		{
			return SimpleAdObjectLookup<TADWrapperObject>.FindAdObjectTypeByGuidStatic(adSession, objectGuid, NullPerformanceDataLogger.Instance);
		}

		// Token: 0x06002FCC RID: 12236 RVA: 0x000C3FC4 File Offset: 0x000C21C4
		public static TADWrapperObject FindAdObjectTypeByGuidStatic(IADToplogyConfigurationSession adSession, Guid objectGuid, IPerformanceDataLogger perfLogger)
		{
			if (objectGuid == Guid.Empty)
			{
				throw new ArgumentException("objectGuid cannot be Empty.");
			}
			TADWrapperObject adObject = default(TADWrapperObject);
			Exception ex = ADUtils.RunADOperation(delegate()
			{
				QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Guid, objectGuid);
				TADWrapperObject[] array = adSession.Find<TADWrapperObject>(null, QueryScope.SubTree, filter, null, 1);
				adObject = ((array != null && array.Length > 0) ? array[0] : default(TADWrapperObject));
			}, perfLogger, 2);
			if (ex != null)
			{
				SimpleAdObjectLookup<TADWrapperObject>.Tracer.TraceError<Exception>(0L, "FindAdObjectTypeByGuidStatic got an exception: {0}", ex);
			}
			return adObject;
		}

		// Token: 0x06002FCD RID: 12237 RVA: 0x000C409C File Offset: 0x000C229C
		public static TADWrapperObject FindAdObjectByServerNameStatic(IADToplogyConfigurationSession adSession, string serverName, out Exception exception)
		{
			if (string.IsNullOrEmpty(serverName))
			{
				throw new ArgumentNullException("serverName");
			}
			exception = null;
			TADWrapperObject adObject = default(TADWrapperObject);
			Exception ex = ADUtils.RunADOperation(delegate()
			{
				QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, serverName);
				TADWrapperObject[] array = adSession.Find<TADWrapperObject>(null, QueryScope.SubTree, filter, null, 1);
				adObject = ((array != null && array.Length > 0) ? array[0] : default(TADWrapperObject));
			}, 2);
			if (ex != null)
			{
				exception = ex;
				SimpleAdObjectLookup<TADWrapperObject>.Tracer.TraceError<Exception>(0L, "FindAdObjectByServerNameStatic got exception: {0}", ex);
			}
			return adObject;
		}

		// Token: 0x06002FCE RID: 12238 RVA: 0x000C4115 File Offset: 0x000C2315
		public void Clear()
		{
		}

		// Token: 0x06002FCF RID: 12239 RVA: 0x000C4118 File Offset: 0x000C2318
		public TADWrapperObject ReadAdObjectByObjectId(ADObjectId objectId)
		{
			Exception ex;
			return this.ReadAdObjectByObjectIdEx(objectId, out ex);
		}

		// Token: 0x06002FD0 RID: 12240 RVA: 0x000C4158 File Offset: 0x000C2358
		public TADWrapperObject ReadAdObjectByObjectIdEx(ADObjectId objectId, out Exception ex)
		{
			TADWrapperObject adObject = default(TADWrapperObject);
			ex = ADUtils.RunADOperation(delegate()
			{
				adObject = this.AdSession.ReadADObject<TADWrapperObject>(objectId);
			}, 2);
			if (ex != null)
			{
				SimpleAdObjectLookup<TADWrapperObject>.Tracer.TraceError<Exception>((long)this.GetHashCode(), "SimpleAdObjectLookup.ReadAdObjectByObjectIdEx got an exception: {0}", ex);
			}
			return adObject;
		}

		// Token: 0x06002FD1 RID: 12241 RVA: 0x000C41BB File Offset: 0x000C23BB
		public TADWrapperObject FindAdObjectByGuid(Guid objectGuid)
		{
			return SimpleAdObjectLookup<TADWrapperObject>.FindAdObjectTypeByGuidStatic(this.AdSession, objectGuid);
		}

		// Token: 0x06002FD2 RID: 12242 RVA: 0x000C41C9 File Offset: 0x000C23C9
		public TADWrapperObject FindAdObjectByGuidEx(Guid objectGuid, AdObjectLookupFlags flags)
		{
			return this.FindAdObjectByGuidEx(objectGuid, flags, NullPerformanceDataLogger.Instance);
		}

		// Token: 0x06002FD3 RID: 12243 RVA: 0x000C41D8 File Offset: 0x000C23D8
		public TADWrapperObject FindAdObjectByGuidEx(Guid objectGuid, AdObjectLookupFlags flags, IPerformanceDataLogger perfLogger)
		{
			return SimpleAdObjectLookup<TADWrapperObject>.FindAdObjectTypeByGuidStatic(this.AdSession, objectGuid, perfLogger);
		}

		// Token: 0x06002FD4 RID: 12244 RVA: 0x000C41E7 File Offset: 0x000C23E7
		public TADWrapperObject FindAdObjectByQuery(QueryFilter queryFilter)
		{
			return SimpleAdObjectLookup<TADWrapperObject>.FindAdObjectWithQueryStatic(this.AdSession, queryFilter);
		}

		// Token: 0x06002FD5 RID: 12245 RVA: 0x000C41F5 File Offset: 0x000C23F5
		public TADWrapperObject FindAdObjectByQueryEx(QueryFilter queryFilter, AdObjectLookupFlags flags)
		{
			return SimpleAdObjectLookup<TADWrapperObject>.FindAdObjectWithQueryStatic(this.AdSession, queryFilter);
		}

		// Token: 0x06002FD6 RID: 12246 RVA: 0x000C4204 File Offset: 0x000C2404
		public TADWrapperObject FindServerByFqdn(string fqdn)
		{
			Exception ex;
			return this.FindServerByFqdnWithException(fqdn, out ex);
		}

		// Token: 0x06002FD7 RID: 12247 RVA: 0x000C421C File Offset: 0x000C241C
		public TADWrapperObject FindServerByFqdnWithException(string fqdn, out Exception ex)
		{
			ex = null;
			string nodeNameFromFqdn = MachineName.GetNodeNameFromFqdn(fqdn);
			return SimpleAdObjectLookup<TADWrapperObject>.FindAdObjectByServerNameStatic(this.AdSession, nodeNameFromFqdn, out ex);
		}
	}
}
