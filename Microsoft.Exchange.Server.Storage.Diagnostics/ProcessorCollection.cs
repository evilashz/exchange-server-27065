using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.Diagnostics
{
	// Token: 0x0200003F RID: 63
	internal static class ProcessorCollection
	{
		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x0000D71C File Offset: 0x0000B91C
		internal static IDictionary<string, ProcessorCollection.QueryableProcessor> Collection
		{
			get
			{
				if (ProcessorCollection.dictionary == null)
				{
					ProcessorCollection.dictionary = ProcessorCollection.CreateCollection();
				}
				return ProcessorCollection.dictionary;
			}
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0000D734 File Offset: 0x0000B934
		public static bool TryGetProcessorFactory(string name, out Func<IList<Column>, Processor> factory)
		{
			factory = null;
			ProcessorCollection.QueryableProcessor queryableProcessor;
			if (ProcessorCollection.Collection.TryGetValue(name, out queryableProcessor))
			{
				factory = queryableProcessor.Factory;
				return true;
			}
			return false;
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0000D75E File Offset: 0x0000B95E
		public static IEnumerable<ProcessorCollection.QueryableProcessor> GetCollection()
		{
			return ProcessorCollection.Collection.Values;
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000D76C File Offset: 0x0000B96C
		private static IDictionary<string, ProcessorCollection.QueryableProcessor> CreateCollection()
		{
			IDictionary<string, ProcessorCollection.QueryableProcessor> dictionary = new Dictionary<string, ProcessorCollection.QueryableProcessor>(10);
			Type typeFromHandle = typeof(ProcessorCollection.QueryableProcessor);
			foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
			{
				if (type.IsSubclassOf(typeof(Processor)))
				{
					foreach (PropertyInfo propertyInfo in type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public))
					{
						MethodInfo getMethod = propertyInfo.GetGetMethod();
						if (getMethod != null && getMethod.ReturnType.Equals(typeFromHandle))
						{
							ProcessorCollection.QueryableProcessor queryableProcessor = (ProcessorCollection.QueryableProcessor)getMethod.Invoke(null, null);
							dictionary[queryableProcessor.Name] = queryableProcessor;
						}
					}
				}
			}
			return dictionary;
		}

		// Token: 0x0400011C RID: 284
		private static IDictionary<string, ProcessorCollection.QueryableProcessor> dictionary;

		// Token: 0x02000040 RID: 64
		internal sealed class QueryableProcessor
		{
			// Token: 0x17000089 RID: 137
			// (get) Token: 0x060001CB RID: 459 RVA: 0x0000D82D File Offset: 0x0000BA2D
			// (set) Token: 0x060001CC RID: 460 RVA: 0x0000D835 File Offset: 0x0000BA35
			[Queryable(Index = 0)]
			public string Name { get; private set; }

			// Token: 0x1700008A RID: 138
			// (get) Token: 0x060001CD RID: 461 RVA: 0x0000D83E File Offset: 0x0000BA3E
			// (set) Token: 0x060001CE RID: 462 RVA: 0x0000D846 File Offset: 0x0000BA46
			[Queryable]
			public string Consumes { get; private set; }

			// Token: 0x1700008B RID: 139
			// (get) Token: 0x060001CF RID: 463 RVA: 0x0000D84F File Offset: 0x0000BA4F
			// (set) Token: 0x060001D0 RID: 464 RVA: 0x0000D857 File Offset: 0x0000BA57
			[Queryable]
			public string Produces { get; private set; }

			// Token: 0x1700008C RID: 140
			// (get) Token: 0x060001D1 RID: 465 RVA: 0x0000D860 File Offset: 0x0000BA60
			// (set) Token: 0x060001D2 RID: 466 RVA: 0x0000D868 File Offset: 0x0000BA68
			[Queryable]
			public string Usage { get; private set; }

			// Token: 0x1700008D RID: 141
			// (get) Token: 0x060001D3 RID: 467 RVA: 0x0000D871 File Offset: 0x0000BA71
			// (set) Token: 0x060001D4 RID: 468 RVA: 0x0000D879 File Offset: 0x0000BA79
			public Func<IList<Column>, Processor> Factory { get; private set; }

			// Token: 0x060001D5 RID: 469 RVA: 0x0000D884 File Offset: 0x0000BA84
			public static ProcessorCollection.QueryableProcessor Create(string name, string consumes, string produces, string usage, Func<IList<Column>, Processor> factory)
			{
				return new ProcessorCollection.QueryableProcessor
				{
					Name = name,
					Consumes = consumes,
					Produces = produces,
					Usage = usage,
					Factory = factory
				};
			}
		}
	}
}
