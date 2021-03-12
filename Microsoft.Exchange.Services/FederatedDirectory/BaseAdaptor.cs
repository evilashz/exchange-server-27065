using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Office.Server.Directory;

namespace Microsoft.Exchange.FederatedDirectory
{
	// Token: 0x02000067 RID: 103
	internal abstract class BaseAdaptor : IDirectoryAdapter
	{
		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600024B RID: 587 RVA: 0x0000C3FE File Offset: 0x0000A5FE
		// (set) Token: 0x0600024C RID: 588 RVA: 0x0000C406 File Offset: 0x0000A606
		public ICollection<PropertyType> PropertyTypes { get; protected set; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600024D RID: 589 RVA: 0x0000C40F File Offset: 0x0000A60F
		// (set) Token: 0x0600024E RID: 590 RVA: 0x0000C417 File Offset: 0x0000A617
		public ICollection<ResourceType> ResourceTypes { get; protected set; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600024F RID: 591 RVA: 0x0000C420 File Offset: 0x0000A620
		// (set) Token: 0x06000250 RID: 592 RVA: 0x0000C428 File Offset: 0x0000A628
		public ICollection<RelationType> RelationTypes { get; protected set; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000251 RID: 593 RVA: 0x0000C431 File Offset: 0x0000A631
		// (set) Token: 0x06000252 RID: 594 RVA: 0x0000C439 File Offset: 0x0000A639
		public Guid AdapterId { get; protected set; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000253 RID: 595 RVA: 0x0000C442 File Offset: 0x0000A642
		// (set) Token: 0x06000254 RID: 596 RVA: 0x0000C44A File Offset: 0x0000A64A
		public string ServiceName { get; protected set; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000255 RID: 597 RVA: 0x0000C453 File Offset: 0x0000A653
		// (set) Token: 0x06000256 RID: 598 RVA: 0x0000C45B File Offset: 0x0000A65B
		public NameValueCollection Parameters { get; protected set; }

		// Token: 0x06000257 RID: 599
		public abstract void Initialize(NameValueCollection parameters);

		// Token: 0x06000258 RID: 600
		public abstract void RemoveDirectoryObject(DirectoryObjectAccessor directoryObjectAccessor);

		// Token: 0x06000259 RID: 601
		public abstract void CommitDirectoryObject(DirectoryObjectAccessor directoryObjectAccessor);

		// Token: 0x0600025A RID: 602
		public abstract void LoadDirectoryObjectData(DirectoryObjectAccessor directoryObjectAccessor, RequestSchema requestSchema, out IDirectoryObjectState state);

		// Token: 0x0600025B RID: 603 RVA: 0x0000C464 File Offset: 0x0000A664
		public IDirectorySessionContext GetDirectorySessionContext()
		{
			return null;
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000C468 File Offset: 0x0000A668
		public IDirectoryObjectState CreateState()
		{
			return new DirectoryObjectState
			{
				IsCommitted = false,
				Version = -1L
			};
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000C48B File Offset: 0x0000A68B
		public virtual void NotifyChanges(DirectoryObjectAccessor directoryObjectAccessor)
		{
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000C48D File Offset: 0x0000A68D
		public virtual bool DirectoryObjectExists(DirectorySession directorySession, string propertyName, object propertyValue, DirectoryObjectType directoryObjectType)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000C494 File Offset: 0x0000A694
		public virtual bool RelationExists(DirectorySession directorySession, string relationName, Guid parentObjectId, DirectoryObjectType parentObjectObjectType, Guid targetObjectId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000C49B File Offset: 0x0000A69B
		public virtual bool TryRelationExists(DirectorySession directorySession, string relationName, Guid parentObjectId, DirectoryObjectType parentObjectObjectType, Guid targetObjectId, out bool relationExists)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000C4A4 File Offset: 0x0000A6A4
		public void LoadDirectoryObjectDataBatch(IList<DirectoryObjectAccessor> directoryObjectAccessors, RequestSchema requestSchema, out IList<IDirectoryObjectState> states)
		{
			states = new IDirectoryObjectState[directoryObjectAccessors.Count];
			for (int i = 0; i < directoryObjectAccessors.Count; i++)
			{
				IDirectoryObjectState value;
				this.LoadDirectoryObjectData(directoryObjectAccessors[i], requestSchema, out value);
				states[i] = value;
			}
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000C4E8 File Offset: 0x0000A6E8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(100);
			stringBuilder.Append(base.GetType().Name);
			stringBuilder.Append(": ");
			BaseAdaptor.AppendToString<PropertyType>(stringBuilder, this.PropertyTypes, "PropertyTypes");
			stringBuilder.Append(";");
			BaseAdaptor.AppendToString<ResourceType>(stringBuilder, this.ResourceTypes, "ResourceTypes");
			stringBuilder.Append(";");
			BaseAdaptor.AppendToString<RelationType>(stringBuilder, this.RelationTypes, "RelationTypes");
			return stringBuilder.ToString();
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000C56C File Offset: 0x0000A76C
		private static void AppendToString<T>(StringBuilder value, ICollection<T> schemaTypes, string name) where T : SchemaType
		{
			value.Append(name);
			value.Append("={");
			bool flag = true;
			foreach (T t in schemaTypes)
			{
				SchemaType schemaType = t;
				if (flag)
				{
					flag = false;
				}
				else
				{
					value.Append(",");
				}
				value.Append(schemaType.Name);
			}
			value.Append("}");
		}

		// Token: 0x0400055D RID: 1373
		protected static readonly Trace Tracer = ExTraceGlobals.FederatedDirectoryTracer;
	}
}
