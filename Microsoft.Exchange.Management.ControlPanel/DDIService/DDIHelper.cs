using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Mapi;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Data.Storage.Management.Migration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Management.ControlPanel;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.ControlPanel;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x0200011C RID: 284
	public static class DDIHelper
	{
		// Token: 0x0600200E RID: 8206 RVA: 0x0006052C File Offset: 0x0005E72C
		static DDIHelper()
		{
			DDIHelper.GetListDefaultResultSize = ConfigUtil.ReadInt("GetListDefaultResultSize", 500);
			DDIHelper.GetListAsyncModeResultSize = ConfigUtil.ReadInt("GetListAsyncModeResultSize", Util.IsDataCenter ? 10000 : 20000);
		}

		// Token: 0x0600200F RID: 8207 RVA: 0x00060740 File Offset: 0x0005E940
		internal static void RegisterStrongType2DataContractMapping(Type strongType, Type dataContractType)
		{
			if (DDIHelper.strongType2DataContractMapping.ContainsKey(strongType))
			{
				throw new ArgumentException("Mapping is already provided for strong type :" + strongType.FullName);
			}
			DDIHelper.strongType2DataContractMapping[strongType] = dataContractType;
		}

		// Token: 0x06002010 RID: 8208 RVA: 0x00060774 File Offset: 0x0005E974
		internal static object PrepareVariableForSerialization(object value, Variable variable)
		{
			value = (DBNull.Value.Equals(value) ? null : value);
			if (variable.OutputConverter != null && variable.OutputConverter.CanConvert(value))
			{
				value = variable.OutputConverter.Convert(value);
			}
			else
			{
				value = DDIHelper.TryStrongTypeConversion(value);
			}
			if (variable.Type == typeof(bool) && value == null)
			{
				value = false;
			}
			else if (variable.Type == typeof(string) && value == null)
			{
				value = string.Empty;
			}
			else if (value is DateTime)
			{
				value = ((DateTime)value).LocalToUserDateTimeString();
			}
			return value;
		}

		// Token: 0x06002011 RID: 8209 RVA: 0x00060820 File Offset: 0x0005EA20
		private static object TryStrongTypeConversion(object obj)
		{
			if (!DDIHelper.IsSkippingStrongTypeConversion(obj))
			{
				Type type = obj.GetType();
				Type type2 = null;
				if (DDIHelper.StrongTypeToStringList.Contains(type))
				{
					obj = obj.ToString();
				}
				else if (type == typeof(MultiValuedProperty<string>))
				{
					obj = ((MultiValuedProperty<string>)obj).ToArray();
				}
				else if (!DDIHelper.strongType2DataContractMapping.TryGetValue(type, out type2))
				{
					foreach (KeyValuePair<Type, Type> keyValuePair in DDIHelper.strongType2DataContractMapping)
					{
						if (keyValuePair.Key.IsAssignableFrom(type))
						{
							type2 = keyValuePair.Value;
							break;
						}
					}
				}
				if (type2 == null && (type.IsEnum || typeof(Uri).IsAssignableFrom(type)))
				{
					obj = obj.ToString();
				}
				else if (type2 != null && type2 != null)
				{
					DDIHelper.Trace("Object: {0} of Type '{1}' is converted to '{2}'.", new object[]
					{
						obj,
						type,
						type2
					});
					obj = Activator.CreateInstance(type2, new object[]
					{
						obj
					});
				}
			}
			return obj;
		}

		// Token: 0x06002012 RID: 8210 RVA: 0x0006095C File Offset: 0x0005EB5C
		internal static bool IsSkippingStrongTypeConversion(object obj)
		{
			if (obj == null)
			{
				return true;
			}
			Type type = obj.GetType();
			return type.IsPrimitive || obj is string || type.IsDataContract() || !type.Namespace.StartsWith("Microsoft.Exchange") || (DDIHelper.HasSerializableAttribute(type) && type.Namespace.StartsWith("Microsoft.Exchange.Management.ControlPanel"));
		}

		// Token: 0x06002013 RID: 8211 RVA: 0x000609BB File Offset: 0x0005EBBB
		internal static bool IsDataContract(this Type t)
		{
			return DDIHelper.HasDataContractAttribute(t) || DDIHelper.HasCollectionDataContractAttribute(t);
		}

		// Token: 0x06002014 RID: 8212 RVA: 0x000609D0 File Offset: 0x0005EBD0
		private static bool HasDataContractAttribute(Type type)
		{
			if (!DDIHelper.dataContractHashtable.ContainsKey(type))
			{
				DDIHelper.dataContractHashtable[type] = (type.GetCustomAttributes(typeof(DataContractAttribute), false).Length > 0);
			}
			return (bool)DDIHelper.dataContractHashtable[type];
		}

		// Token: 0x06002015 RID: 8213 RVA: 0x00060A20 File Offset: 0x0005EC20
		private static bool HasCollectionDataContractAttribute(Type type)
		{
			if (!DDIHelper.collectionDataContractHashtable.ContainsKey(type))
			{
				DDIHelper.collectionDataContractHashtable[type] = (type.GetCustomAttributes(typeof(CollectionDataContractAttribute), false).Length > 0);
			}
			return (bool)DDIHelper.collectionDataContractHashtable[type];
		}

		// Token: 0x06002016 RID: 8214 RVA: 0x00060A70 File Offset: 0x0005EC70
		private static bool HasSerializableAttribute(Type type)
		{
			if (!DDIHelper.serializableHashtable.ContainsKey(type))
			{
				DDIHelper.serializableHashtable[type] = (type.GetCustomAttributes(typeof(SerializableAttribute), false).Length > 0);
			}
			return (bool)DDIHelper.serializableHashtable[type];
		}

		// Token: 0x06002017 RID: 8215 RVA: 0x00060AC0 File Offset: 0x0005ECC0
		internal static object ConvertDBNullToNull(DataRow row, string parameterName)
		{
			object obj = row[parameterName];
			if (DBNull.Value.Equals(obj) && row.Table != null)
			{
				obj = null;
			}
			return obj;
		}

		// Token: 0x06002018 RID: 8216 RVA: 0x00060AED File Offset: 0x0005ECED
		internal static void CheckDataTableForSingleObject(DataTable dataTable)
		{
			if (dataTable.Rows.Count != 1)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06002019 RID: 8217 RVA: 0x00060B03 File Offset: 0x0005ED03
		internal static DataRow GetLambdaExpressionDataRow(DataTable dataTable)
		{
			if (dataTable.Rows.Count != 1)
			{
				return null;
			}
			return dataTable.Rows[0];
		}

		// Token: 0x0600201A RID: 8218 RVA: 0x00060B24 File Offset: 0x0005ED24
		internal static object GetVariableValue(ICollection<string> modifiedColumns, string variableName, DataRow input, DataTable dataTable, bool isGetListWorkflow)
		{
			if (modifiedColumns.Contains(variableName))
			{
				object obj = DDIHelper.ConvertDBNullToNull(input, variableName);
				if (obj != null || isGetListWorkflow)
				{
					return obj;
				}
			}
			DDIHelper.CheckDataTableForSingleObject(dataTable);
			return DDIHelper.ConvertDBNullToNull(dataTable.Rows[0], variableName);
		}

		// Token: 0x0600201B RID: 8219 RVA: 0x00060B63 File Offset: 0x0005ED63
		internal static object GetVariableValue(VariableReference variableReference, DataRow input, DataTable dataTable)
		{
			if (variableReference.UseInput)
			{
				return DDIHelper.ConvertDBNullToNull(input, variableReference.Variable);
			}
			DDIHelper.CheckDataTableForSingleObject(dataTable);
			return DDIHelper.ConvertDBNullToNull(dataTable.Rows[0], variableReference.Variable);
		}

		// Token: 0x0600201C RID: 8220 RVA: 0x00060B98 File Offset: 0x0005ED98
		internal static List<string> GetCodeBehindRegisteredDepVariables(DataColumn column)
		{
			List<string> list = new List<string>();
			List<string> list2 = column.ExtendedProperties["RbacMetaData"] as List<string>;
			if (list2 != null)
			{
				list.AddRange(list2);
			}
			return list;
		}

		// Token: 0x0600201D RID: 8221 RVA: 0x00060BCC File Offset: 0x0005EDCC
		internal static List<string> GetOutputDepVariables(DataColumn column)
		{
			List<string> list = new List<string>();
			Variable variable = column.ExtendedProperties["Variable"] as Variable;
			if (variable != null)
			{
				string text = variable.Value as string;
				if (DDIHelper.IsLambdaExpression(text))
				{
					list = ExpressionCalculator.BuildColumnExpression(text).DependentColumns;
				}
				else
				{
					list.AddRange(DDIHelper.GetCodeBehindRegisteredDepVariables(column));
				}
			}
			return list;
		}

		// Token: 0x0600201E RID: 8222 RVA: 0x00060C28 File Offset: 0x0005EE28
		internal static IEnumerable<string> GetOutputRawVariables(IEnumerable<DataColumn> columns)
		{
			List<string> list = new List<string>();
			foreach (DataColumn dataColumn in columns)
			{
				List<string> outputDepVariables = DDIHelper.GetOutputDepVariables(dataColumn);
				if (outputDepVariables == null || outputDepVariables.Count == 0)
				{
					list.Add(dataColumn.ColumnName);
				}
				else
				{
					list.AddRange(outputDepVariables);
				}
			}
			return list.Distinct<string>();
		}

		// Token: 0x0600201F RID: 8223 RVA: 0x00060C9C File Offset: 0x0005EE9C
		internal static string JoinList<T>(IEnumerable<T> list, Func<T, string> formatter)
		{
			if (list == null)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (T t in list)
			{
				if (t != null)
				{
					if (stringBuilder.Length != 0)
					{
						stringBuilder.Append(ClientStrings.ListSeparator);
					}
					stringBuilder.Append(formatter(t));
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002020 RID: 8224 RVA: 0x00060D1C File Offset: 0x0005EF1C
		internal static bool IsLambdaExpression(string value)
		{
			return !string.IsNullOrEmpty(value) && value.Contains("=>");
		}

		// Token: 0x06002021 RID: 8225 RVA: 0x00060D34 File Offset: 0x0005EF34
		public static string GetLogonName(string dn, string samAccountName)
		{
			ADObjectId adobjectId = new ADObjectId(dn, Guid.Empty);
			return adobjectId.DomainId.ToString() + "\\" + samAccountName;
		}

		// Token: 0x06002022 RID: 8226 RVA: 0x00060D63 File Offset: 0x0005EF63
		public static ExTimeZone GetUserTimeZone()
		{
			return EacRbacPrincipal.Instance.UserTimeZone ?? ExTimeZone.UtcTimeZone;
		}

		// Token: 0x06002023 RID: 8227 RVA: 0x00060D78 File Offset: 0x0005EF78
		public static bool IsEmptyValue(object propertyValue)
		{
			bool result = false;
			if (propertyValue == null)
			{
				result = true;
			}
			else if (DBNull.Value.Equals(propertyValue))
			{
				result = true;
			}
			else if (propertyValue is IEnumerable && DDIHelper.IsEmptyCollection(propertyValue as IEnumerable))
			{
				result = true;
			}
			else if (propertyValue is Guid && Guid.Empty.Equals(propertyValue))
			{
				result = true;
			}
			else if (string.IsNullOrEmpty(propertyValue.ToString()))
			{
				result = true;
			}
			return result;
		}

		// Token: 0x06002024 RID: 8228 RVA: 0x00060DEA File Offset: 0x0005EFEA
		internal static bool IsLegacyObject(IVersionable versionableObjct)
		{
			return versionableObjct.ExchangeVersion.ExchangeBuild.Major < versionableObjct.MaximumSupportedExchangeObjectVersion.ExchangeBuild.Major;
		}

		// Token: 0x06002025 RID: 8229 RVA: 0x00060E10 File Offset: 0x0005F010
		public static bool IsUnlimited(object value)
		{
			if (DDIHelper.IsEmptyValue(value))
			{
				return false;
			}
			Type type = value.GetType();
			return (bool)type.GetProperty("IsUnlimited").GetValue(value, null);
		}

		// Token: 0x06002026 RID: 8230 RVA: 0x00060E45 File Offset: 0x0005F045
		public static string AsteriskAround(string inString)
		{
			if (!string.IsNullOrEmpty(inString))
			{
				return "*" + inString + "*";
			}
			return "*";
		}

		// Token: 0x06002027 RID: 8231 RVA: 0x00060E68 File Offset: 0x0005F068
		public static bool IsFFO()
		{
			bool result;
			try
			{
				result = EacEnvironment.Instance.IsForefrontForOffice;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06002028 RID: 8232 RVA: 0x00060E98 File Offset: 0x0005F098
		private static bool IsEmptyCollection(IEnumerable enumerable)
		{
			bool result = true;
			if (enumerable != null)
			{
				using (IEnumerator enumerator = enumerable.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						result = false;
					}
				}
			}
			return result;
		}

		// Token: 0x06002029 RID: 8233 RVA: 0x00060EEC File Offset: 0x0005F0EC
		internal static void Trace(TraceType traceType, string msg)
		{
			switch (traceType)
			{
			case TraceType.DebugTrace:
				ExTraceGlobals.DDITracer.TraceDebug(0, 0L, msg);
				break;
			case TraceType.WarningTrace:
				ExTraceGlobals.DDITracer.TraceWarning(0, 0L, msg);
				break;
			case TraceType.ErrorTrace:
				ExTraceGlobals.DDITracer.TraceError(0, 0L, msg);
				break;
			case TraceType.InfoTrace:
				ExTraceGlobals.DDITracer.TraceInformation(0, 0L, msg);
				break;
			case TraceType.PerformanceTrace:
				ExTraceGlobals.DDITracer.TracePerformance(0, 0L, msg);
				break;
			case TraceType.FunctionTrace:
				ExTraceGlobals.DDITracer.TraceFunction(0, 0L, msg);
				break;
			case TraceType.PfdTrace:
				ExTraceGlobals.DDITracer.TracePfd(0, 0L, msg);
				break;
			}
			if (DDIHelper.IsWebTraceEnabled() && HttpContext.Current.ApplicationInstance != null)
			{
				HttpContext.Current.Trace.Write(msg);
			}
		}

		// Token: 0x0600202A RID: 8234 RVA: 0x00060FB6 File Offset: 0x0005F1B6
		internal static void Trace(string msg)
		{
			DDIHelper.Trace(TraceType.DebugTrace, msg);
		}

		// Token: 0x0600202B RID: 8235 RVA: 0x00060FBF File Offset: 0x0005F1BF
		internal static void Trace<T>(TraceType traceType, T obj)
		{
			if (obj != null && DDIHelper.HasTraceEnabled(traceType))
			{
				DDIHelper.Trace(traceType, EcpTraceHelper.GetTraceString(obj));
			}
		}

		// Token: 0x0600202C RID: 8236 RVA: 0x00060FE2 File Offset: 0x0005F1E2
		internal static void Trace<T>(T obj)
		{
			DDIHelper.Trace<T>(TraceType.DebugTrace, obj);
		}

		// Token: 0x0600202D RID: 8237 RVA: 0x00060FEC File Offset: 0x0005F1EC
		internal static void Trace(TraceType traceType, string formatString, params object[] args)
		{
			if (formatString != null && DDIHelper.HasTraceEnabled(traceType))
			{
				List<string> list = new List<string>();
				if (args != null)
				{
					foreach (object obj in args)
					{
						list.Add((obj == null) ? string.Empty : EcpTraceHelper.GetTraceString(obj));
					}
				}
				DDIHelper.Trace(traceType, string.Format(formatString, list.ToArray()));
			}
		}

		// Token: 0x0600202E RID: 8238 RVA: 0x00061049 File Offset: 0x0005F249
		internal static void Trace(string formatString, params object[] args)
		{
			DDIHelper.Trace(TraceType.DebugTrace, formatString, args);
		}

		// Token: 0x0600202F RID: 8239 RVA: 0x00061054 File Offset: 0x0005F254
		internal static string ToQuotationEscapedString(object item)
		{
			string result = string.Empty;
			if (item != null)
			{
				result = item.ToString().Replace("'", "''");
			}
			return result;
		}

		// Token: 0x06002030 RID: 8240 RVA: 0x00061081 File Offset: 0x0005F281
		private static bool HasTraceEnabled(TraceType traceType)
		{
			return ExTraceGlobals.DDITracer.IsTraceEnabled(traceType) || DDIHelper.IsWebTraceEnabled();
		}

		// Token: 0x06002031 RID: 8241 RVA: 0x00061097 File Offset: 0x0005F297
		private static bool IsWebTraceEnabled()
		{
			return HttpContext.Current != null && HttpContext.Current.Trace.IsEnabled;
		}

		// Token: 0x06002032 RID: 8242 RVA: 0x000610B1 File Offset: 0x0005F2B1
		internal static void CheckSchemaName(string schemalName)
		{
			if (DDIHelper.schemaNamePattern == null)
			{
				DDIHelper.schemaNamePattern = new Regex("^[0-9a-z]+$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
			}
			if (!DDIHelper.schemaNamePattern.IsMatch(schemalName))
			{
				throw new BadRequestException(new Exception("Invalid schema name: " + schemalName));
			}
		}

		// Token: 0x17001A35 RID: 6709
		// (get) Token: 0x06002033 RID: 8243 RVA: 0x000610EE File Offset: 0x0005F2EE
		internal static bool ForGetListProgress
		{
			get
			{
				return !string.IsNullOrEmpty(DDIHelper.ProgressIdForGetListAsync);
			}
		}

		// Token: 0x17001A36 RID: 6710
		// (get) Token: 0x06002034 RID: 8244 RVA: 0x00061100 File Offset: 0x0005F300
		// (set) Token: 0x06002035 RID: 8245 RVA: 0x0006112D File Offset: 0x0005F32D
		internal static string ProgressIdForGetListAsync
		{
			get
			{
				HttpContext httpContext = HttpContext.Current;
				if (httpContext == null)
				{
					return null;
				}
				return httpContext.Items["ProgressId"] as string;
			}
			set
			{
				HttpContext.Current.Items["ProgressId"] = value;
			}
		}

		// Token: 0x17001A37 RID: 6711
		// (get) Token: 0x06002036 RID: 8246 RVA: 0x00061144 File Offset: 0x0005F344
		public static bool IsGetListAsync
		{
			get
			{
				HttpContext httpContext = HttpContext.Current;
				return httpContext != null && httpContext.Request.QueryString["getlistasync"] == "1";
			}
		}

		// Token: 0x17001A38 RID: 6712
		// (get) Token: 0x06002037 RID: 8247 RVA: 0x0006117C File Offset: 0x0005F37C
		public static bool IsGetListPreLoad
		{
			get
			{
				HttpContext httpContext = HttpContext.Current;
				return httpContext != null && object.Equals(httpContext.Items["getlistasync"], "1");
			}
		}

		// Token: 0x06002038 RID: 8248 RVA: 0x000611AE File Offset: 0x0005F3AE
		public static Hashtable ConvertToAddHashTable(object identities)
		{
			return DDIHelper.ConvertToHashTable(identities, "add");
		}

		// Token: 0x06002039 RID: 8249 RVA: 0x000611BB File Offset: 0x0005F3BB
		public static Hashtable ConvertToRemoveHashTable(object identities)
		{
			return DDIHelper.ConvertToHashTable(identities, "remove");
		}

		// Token: 0x0600203A RID: 8250 RVA: 0x000611C8 File Offset: 0x0005F3C8
		private static Hashtable ConvertToHashTable(object identities, string addOrRemoveforKeyName)
		{
			Hashtable result = new Hashtable();
			if (identities == null || identities == DBNull.Value)
			{
				return result;
			}
			object[] array = identities as object[];
			if (array == null || array.Length == 0)
			{
				DDIHelper.ThrowIllegalInput();
			}
			if (string.IsNullOrWhiteSpace(addOrRemoveforKeyName) || (!addOrRemoveforKeyName.Equals("add", StringComparison.OrdinalIgnoreCase) && !addOrRemoveforKeyName.Equals("remove", StringComparison.OrdinalIgnoreCase)))
			{
				DDIHelper.ThrowIllegalHashtableKeyType(addOrRemoveforKeyName);
			}
			Hashtable hashtable = new Hashtable();
			string[] array2 = new string[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				Identity identity = array[i] as Identity;
				if (identity == null)
				{
					DDIHelper.ThrowIllegalInput();
				}
				array2[i] = identity.RawIdentity;
			}
			hashtable.Add(addOrRemoveforKeyName, array2);
			return hashtable;
		}

		// Token: 0x0600203B RID: 8251 RVA: 0x00061275 File Offset: 0x0005F475
		private static void ThrowIllegalInput()
		{
			throw new ArgumentException("Expected object array of Microsoft.Exchange.Management.ControlPanel.Identity to be converted into Hashtable for add or remove bulk operation");
		}

		// Token: 0x0600203C RID: 8252 RVA: 0x00061284 File Offset: 0x0005F484
		private static void ThrowIllegalHashtableKeyType(string illegalHashtableKeynameUsed)
		{
			if (string.IsNullOrEmpty(illegalHashtableKeynameUsed))
			{
				illegalHashtableKeynameUsed = "<null or empty>";
			}
			string message = string.Format("Expected 'add' or 'remove' for keyname to hashtable. Provided keyname: {0}", illegalHashtableKeynameUsed);
			throw new ArgumentException(message);
		}

		// Token: 0x04001CB7 RID: 7351
		internal const string ProgressIdHttpContextKey = "ProgressId";

		// Token: 0x04001CB8 RID: 7352
		internal const string GetListAsyncUrlParameterKey = "getlistasync";

		// Token: 0x04001CB9 RID: 7353
		public static readonly int GetListDefaultResultSize;

		// Token: 0x04001CBA RID: 7354
		public static readonly int GetListAsyncModeResultSize;

		// Token: 0x04001CBB RID: 7355
		private static readonly Dictionary<Type, Type> strongType2DataContractMapping = new Dictionary<Type, Type>
		{
			{
				typeof(ProxyAddressTemplateCollection),
				typeof(AddressTemplateList)
			},
			{
				typeof(ProxyAddressCollection),
				typeof(EmailAddressList)
			},
			{
				typeof(DatabaseAvailabilityGroupNetworkSubnet),
				typeof(DAGNetworkSubnetItem)
			},
			{
				typeof(DatabaseAvailabilityGroupNetworkInterface),
				typeof(DAGNetworkInterfaceItem)
			},
			{
				typeof(DagNetworkObjectId),
				typeof(Identity)
			},
			{
				typeof(ADObjectId),
				typeof(Identity)
			},
			{
				typeof(MailboxId),
				typeof(MailboxIdentity)
			},
			{
				typeof(AppId),
				typeof(Identity)
			},
			{
				typeof(MigrationBatchId),
				typeof(Identity)
			},
			{
				typeof(MigrationUserId),
				typeof(Identity)
			},
			{
				typeof(MigrationEndpointId),
				typeof(Identity)
			},
			{
				typeof(MigrationReportId),
				typeof(Identity)
			},
			{
				typeof(MigrationStatisticsId),
				typeof(Identity)
			},
			{
				typeof(ServerVersion),
				typeof(EcpServerVersion)
			}
		};

		// Token: 0x04001CBC RID: 7356
		private static readonly List<Type> StrongTypeToStringList = new List<Type>
		{
			typeof(ServerVersion),
			typeof(SmtpAddress)
		};

		// Token: 0x04001CBD RID: 7357
		private static readonly Hashtable dataContractHashtable = Hashtable.Synchronized(new Hashtable());

		// Token: 0x04001CBE RID: 7358
		private static readonly Hashtable collectionDataContractHashtable = Hashtable.Synchronized(new Hashtable());

		// Token: 0x04001CBF RID: 7359
		private static readonly Hashtable serializableHashtable = Hashtable.Synchronized(new Hashtable());

		// Token: 0x04001CC0 RID: 7360
		private static Regex schemaNamePattern;
	}
}
