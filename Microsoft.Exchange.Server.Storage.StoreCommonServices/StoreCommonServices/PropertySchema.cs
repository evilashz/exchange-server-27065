using System;
using System.Collections.Generic;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PropTags;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x020000FB RID: 251
	internal class PropertySchema
	{
		// Token: 0x060009D5 RID: 2517 RVA: 0x0002EC7F File Offset: 0x0002CE7F
		private PropertySchema()
		{
			this.objectSchemas = new Dictionary<int, ObjectPropertySchema>(10);
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x0002EC94 File Offset: 0x0002CE94
		internal static void Initialize()
		{
			if (PropertySchema.dataSlot == -1)
			{
				PropertySchema.dataSlot = StoreDatabase.AllocateComponentDataSlot();
			}
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x0002ECA8 File Offset: 0x0002CEA8
		internal static void MountEventHandler(StoreDatabase database)
		{
			PropertySchema value = new PropertySchema();
			database.ComponentData[PropertySchema.dataSlot] = value;
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x0002ECCC File Offset: 0x0002CECC
		internal static void DismountEventHandler(StoreDatabase database)
		{
			database.ComponentData[PropertySchema.dataSlot] = null;
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x0002ECE0 File Offset: 0x0002CEE0
		internal static PropertySchema GetSchema(StoreDatabase database)
		{
			return database.ComponentData[PropertySchema.dataSlot] as PropertySchema;
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x0002ED04 File Offset: 0x0002CF04
		public static void AddObjectSchema(StoreDatabase database, ObjectType objectType, ObjectPropertySchema objectSchema)
		{
			PropertySchema schema = PropertySchema.GetSchema(database);
			schema.AddObjectSchema(objectType, objectSchema);
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x0002ED20 File Offset: 0x0002CF20
		internal static ObjectPropertySchema GetObjectSchema(StoreDatabase database, ObjectType objectType)
		{
			PropertySchema schema = PropertySchema.GetSchema(database);
			return schema.GetObjectSchema(objectType);
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x0002ED3C File Offset: 0x0002CF3C
		public static PropertyMapping FindMapping(StoreDatabase database, ObjectType objectType, StorePropTag propertyTag)
		{
			ObjectPropertySchema objectSchema = PropertySchema.GetObjectSchema(database, objectType);
			if (objectSchema == null)
			{
				return null;
			}
			return objectSchema.FindMapping(propertyTag);
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x0002ED60 File Offset: 0x0002CF60
		public static Column MapToColumn(StoreDatabase database, ObjectType objectType, StorePropTag propertyTag)
		{
			ObjectPropertySchema objectSchema = PropertySchema.GetObjectSchema(database, objectType);
			if (objectSchema == null)
			{
				return null;
			}
			return PropertySchema.MapToColumnHelper(objectSchema, propertyTag);
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x0002ED84 File Offset: 0x0002CF84
		public static bool IsMultiValueInstanceColumn(Column column)
		{
			ExtendedPropertyColumn extendedPropertyColumn;
			return PropertySchema.IsMultiValueInstanceColumn(column, out extendedPropertyColumn);
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x0002ED9C File Offset: 0x0002CF9C
		public static bool IsMultiValueInstanceColumn(Column column, out ExtendedPropertyColumn baseMultiValueColumn)
		{
			baseMultiValueColumn = null;
			MappedPropertyColumn mappedPropertyColumn = column as MappedPropertyColumn;
			if (mappedPropertyColumn == null)
			{
				return false;
			}
			if (!mappedPropertyColumn.StorePropTag.IsMultiValueInstance)
			{
				return false;
			}
			FunctionColumn functionColumn = mappedPropertyColumn.ActualColumn as FunctionColumn;
			baseMultiValueColumn = (functionColumn.ArgumentColumns[1] as ExtendedPropertyColumn);
			return true;
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x0002EDEC File Offset: 0x0002CFEC
		private static Column MapToColumnHelper(ObjectPropertySchema objectSchema, StorePropTag propertyTag)
		{
			PropertyMapping propertyMapping = objectSchema.FindMapping(propertyTag);
			if (propertyMapping != null)
			{
				return propertyMapping.Column;
			}
			ObjectPropertySchema baseSchema = objectSchema.BaseSchema;
			if (baseSchema != null)
			{
				return PropertySchema.MapToColumnHelper(baseSchema, propertyTag);
			}
			if (propertyTag.IsMultiValueInstance)
			{
				return PropertySchema.ConstructMVIFunctionColumn(objectSchema, propertyTag);
			}
			return PropertySchemaPopulation.ConstructPropertyColumn(objectSchema.Table, propertyTag, objectSchema.RowPropBagCreator, null);
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x0002EE44 File Offset: 0x0002D044
		public static Column ConstructMVIFunctionColumn(Column multiValueColumn, Column instanceNumberColumn)
		{
			Type type = null;
			string functionName = null;
			switch (multiValueColumn.ExtendedTypeCode)
			{
			case ExtendedTypeCode.MVInt16:
				functionName = "Exchange.multiValueInstanceProperty_smallint";
				type = typeof(short);
				break;
			case ExtendedTypeCode.MVInt32:
				functionName = "Exchange.multiValueInstanceProperty_int";
				type = typeof(int);
				break;
			case ExtendedTypeCode.MVInt64:
				functionName = "Exchange.multiValueInstanceProperty_bigint";
				type = typeof(long);
				break;
			case ExtendedTypeCode.MVSingle:
				functionName = "Exchange.multiValueInstanceProperty_single";
				type = typeof(float);
				break;
			case ExtendedTypeCode.MVDouble:
				functionName = "Exchange.multiValueInstanceProperty_double";
				type = typeof(double);
				break;
			case ExtendedTypeCode.MVDateTime:
				functionName = "Exchange.multiValueInstanceProperty_datetime";
				type = typeof(DateTime);
				break;
			case ExtendedTypeCode.MVGuid:
				functionName = "Exchange.multiValueInstanceProperty_UNIQUEIDENTIFIER";
				type = typeof(Guid);
				break;
			case ExtendedTypeCode.MVString:
				functionName = "Exchange.multiValueInstanceProperty_nvarchar";
				type = typeof(string);
				break;
			case ExtendedTypeCode.MVBinary:
				functionName = "Exchange.multiValueInstanceProperty_binary";
				type = typeof(byte[]);
				break;
			default:
				Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(false, string.Format("Unexpected type {0}", multiValueColumn.ExtendedTypeCode));
				break;
			}
			PropertyType inType = PropertyTypeHelper.PropTypeFromClrType(type);
			return Factory.CreateFunctionColumn("MultiValueInstanceProperty", type, PropertyTypeHelper.SizeFromPropType(inType), PropertyTypeHelper.MaxLengthFromPropType(inType), multiValueColumn.Table, PropertySchema.multiValueInstanceDelegate, functionName, new Column[]
			{
				instanceNumberColumn,
				multiValueColumn
			});
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x0002EF9C File Offset: 0x0002D19C
		private static Column ConstructMVIFunctionColumn(ObjectPropertySchema objectSchema, StorePropTag propertyTag)
		{
			Column instanceNumberColumn = PropertySchema.MapToColumnHelper(objectSchema, PropTag.Message.InstanceNum);
			Column multiValueColumn = PropertySchema.MapToColumnHelper(objectSchema, propertyTag.ChangeType(propertyTag.PropType & (PropertyType)57343));
			return Factory.CreateMappedPropertyColumn(PropertySchema.ConstructMVIFunctionColumn(multiValueColumn, instanceNumberColumn), propertyTag);
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x0002EFE0 File Offset: 0x0002D1E0
		private ObjectPropertySchema GetObjectSchema(ObjectType objectType)
		{
			ObjectPropertySchema result;
			this.objectSchemas.TryGetValue((int)objectType, out result);
			return result;
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x0002EFFD File Offset: 0x0002D1FD
		private void AddObjectSchema(ObjectType objectType, ObjectPropertySchema objectSchema)
		{
			this.objectSchemas.Add((int)objectType, objectSchema);
		}

		// Token: 0x04000576 RID: 1398
		private static int dataSlot = -1;

		// Token: 0x04000577 RID: 1399
		private static Func<object[], object> multiValueInstanceDelegate = delegate(object[] parameters)
		{
			if (parameters[0] == null || parameters[1] == null)
			{
				return null;
			}
			Array array = (Array)parameters[1];
			if (array.Length == 0)
			{
				return null;
			}
			int num = (int)parameters[0];
			if (num == 0 || num > array.Length)
			{
				return null;
			}
			return array.GetValue(num - 1);
		};

		// Token: 0x04000578 RID: 1400
		private readonly Dictionary<int, ObjectPropertySchema> objectSchemas;
	}
}
