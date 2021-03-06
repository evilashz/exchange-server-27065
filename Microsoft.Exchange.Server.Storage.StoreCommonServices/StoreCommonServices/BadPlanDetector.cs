using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PhysicalAccessSql;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x02000137 RID: 311
	public class BadPlanDetector
	{
		// Token: 0x06000BDD RID: 3037 RVA: 0x0003C5D2 File Offset: 0x0003A7D2
		public BadPlanDetector(StoreDatabase database)
		{
			this.database = database;
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000BDE RID: 3038 RVA: 0x0003C5E1 File Offset: 0x0003A7E1
		// (set) Token: 0x06000BDF RID: 3039 RVA: 0x0003C5E8 File Offset: 0x0003A7E8
		public static bool ShouldSendWatsonReports
		{
			get
			{
				return BadPlanDetector.shouldSendWatsonReports;
			}
			set
			{
				BadPlanDetector.shouldSendWatsonReports = value;
			}
		}

		// Token: 0x06000BE0 RID: 3040 RVA: 0x0003C5F0 File Offset: 0x0003A7F0
		public static void ClearReportedHashes()
		{
			BadPlanDetector.hashesAlreadyReported = new LockFreeDictionary<int, bool>();
		}

		// Token: 0x06000BE1 RID: 3041 RVA: 0x0003C5FC File Offset: 0x0003A7FC
		public void LookForBadPlans(Func<bool> shouldCallbackContinue)
		{
			using (Connection connection = Factory.CreateConnection(null, this.database.PhysicalDatabase, "BadPlanDetector"))
			{
				using (SqlReader sqlReader = this.QueryPlanCache(connection))
				{
					while (shouldCallbackContinue() && sqlReader.Read())
					{
						string badPlanOperators;
						if (this.IsBadPlan(sqlReader.GetStringByOrdinal(0), out badPlanOperators))
						{
							this.ReportBadPlan(sqlReader.GetStringByOrdinal(0), sqlReader.GetStringByOrdinal(1), badPlanOperators);
						}
					}
				}
			}
		}

		// Token: 0x06000BE2 RID: 3042 RVA: 0x0003C694 File Offset: 0x0003A894
		internal static void LookForBadPlans(Context context, StoreDatabase database, Func<bool> shouldCallbackContinue)
		{
			bool flag = ExTraceGlobals.BadPlanDetectionTracer.IsTraceEnabled(TraceType.DebugTrace);
			if (flag && shouldCallbackContinue())
			{
				database.GetSharedLock(context.Diagnostics);
				try
				{
					if (database.IsOnlineActive)
					{
						BadPlanDetector badPlanDetector = new BadPlanDetector(database);
						badPlanDetector.LookForBadPlans(shouldCallbackContinue);
					}
					else if (ExTraceGlobals.BadPlanDetectionTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						ExTraceGlobals.BadPlanDetectionTracer.TraceError<Guid>(0L, "Could not connect to database {0}.  Skipping bad plan detection.", database.MdbGuid);
					}
				}
				catch (SqlException ex)
				{
					context.OnExceptionCatch(ex);
					if (ExTraceGlobals.BadPlanDetectionTracer.IsTraceEnabled(TraceType.ErrorTrace))
					{
						ExTraceGlobals.BadPlanDetectionTracer.TraceError<Type, string, string>(0L, "SqlException while looking for bad plans. Type:[{0}] Message:[{1}] StackTrace:[{2}]", ex.GetType(), ex.Message, ex.StackTrace);
					}
				}
				catch (StoreException ex2)
				{
					context.OnExceptionCatch(ex2);
					if (ExTraceGlobals.BadPlanDetectionTracer.IsTraceEnabled(TraceType.ErrorTrace))
					{
						ExTraceGlobals.BadPlanDetectionTracer.TraceError<Type, string, string>(0L, "Exception while looking for bad plans. Type:[{0}] Message:[{1}] StackTrace:[{2}]", ex2.GetType(), ex2.Message, ex2.StackTrace);
					}
				}
				finally
				{
					database.ReleaseSharedLock();
				}
			}
		}

		// Token: 0x06000BE3 RID: 3043 RVA: 0x0003C7B0 File Offset: 0x0003A9B0
		internal static void MountEventHandler(StoreDatabase database)
		{
			RecurringTask<StoreDatabase> task = new RecurringTask<StoreDatabase>(TaskExecutionWrapper<StoreDatabase>.WrapExecute(new TaskDiagnosticInformation(TaskTypeId.BadPlanDetector, ClientType.System, database.MdbGuid), new TaskExecutionWrapper<StoreDatabase>.TaskCallback<Context>(BadPlanDetector.LookForBadPlans)), database, BadPlanDetector.DetectionInitialDelay, BadPlanDetector.DetectionInterval, false);
			database.TaskList.Add(task, true);
		}

		// Token: 0x06000BE4 RID: 3044 RVA: 0x0003C7FC File Offset: 0x0003A9FC
		private SqlReader QueryPlanCache(Connection connection)
		{
			SqlReader result;
			using (Microsoft.Exchange.Server.Storage.PhysicalAccessSql.SqlCommand sqlCommand = new Microsoft.Exchange.Server.Storage.PhysicalAccessSql.SqlCommand(connection, "SELECT P.query_plan, S.text FROM sys.dm_exec_query_stats T CROSS APPLY sys.dm_exec_query_plan(T.plan_handle) P CROSS APPLY sys.dm_exec_sql_text(T.sql_handle) S", Connection.OperationType.Query))
			{
				result = (SqlReader)sqlCommand.ExecuteReader(Connection.TransactionOption.DontNeedTransaction, 0, null, false);
			}
			return result;
		}

		// Token: 0x06000BE5 RID: 3045 RVA: 0x0003C844 File Offset: 0x0003AA44
		private bool IsBadPlan(string sqlShowplanXml, out string badPlanOperators)
		{
			badPlanOperators = null;
			if (string.IsNullOrEmpty(sqlShowplanXml))
			{
				return false;
			}
			StringBuilder stringBuilder = null;
			bool result;
			try
			{
				bool flag = false;
				bool flag2 = false;
				using (StringReader stringReader = new StringReader(sqlShowplanXml))
				{
					using (XmlTextReader xmlTextReader = SafeXmlFactory.CreateSafeXmlTextReader(stringReader))
					{
						while (xmlTextReader.Read())
						{
							if (xmlTextReader.NodeType == XmlNodeType.Element)
							{
								if (string.Equals(xmlTextReader.Name, "RelOp", StringComparison.Ordinal))
								{
									string text = null;
									if (xmlTextReader.MoveToAttribute("PhysicalOp"))
									{
										text = xmlTextReader.Value;
									}
									string text2 = null;
									if (xmlTextReader.MoveToAttribute("LogicalOp"))
									{
										text2 = xmlTextReader.Value;
									}
									if (BadPlanDetector.BadPhysicalOperators.Contains(text))
									{
										if (stringBuilder == null)
										{
											stringBuilder = new StringBuilder(128);
										}
										else
										{
											stringBuilder.Append("_");
										}
										stringBuilder.AppendFormat("P.{0}", text);
										flag2 = true;
									}
									else if (BadPlanDetector.BadLogicalOperators.Contains(text2))
									{
										if (stringBuilder == null)
										{
											stringBuilder = new StringBuilder(128);
										}
										else
										{
											stringBuilder.Append("_");
										}
										stringBuilder.AppendFormat("L.{0}", text2);
										flag2 = true;
									}
									else
									{
										foreach (KeyValuePair<string, string> keyValuePair in BadPlanDetector.BadOperatorPairs)
										{
											if (string.Equals(keyValuePair.Key, text, StringComparison.Ordinal) && string.Equals(keyValuePair.Value, text2, StringComparison.Ordinal))
											{
												if (stringBuilder == null)
												{
													stringBuilder = new StringBuilder(128);
												}
												else
												{
													stringBuilder.Append("_");
												}
												stringBuilder.AppendFormat("P.{0}.L.{1}", text, text2);
												flag2 = true;
												break;
											}
										}
									}
								}
								else if (string.Equals(xmlTextReader.Name, "StmtSimple", StringComparison.Ordinal) || string.Equals(xmlTextReader.Name, "StmtCond", StringComparison.Ordinal) || string.Equals(xmlTextReader.Name, "StmtCursor", StringComparison.Ordinal) || string.Equals(xmlTextReader.Name, "StmtReceive", StringComparison.Ordinal) || string.Equals(xmlTextReader.Name, "StmtUseDb", StringComparison.Ordinal))
								{
									string text3 = null;
									if (xmlTextReader.MoveToAttribute("StatementText"))
									{
										text3 = xmlTextReader.Value;
									}
									if (text3 != null)
									{
										if (text3.IndexOf("Exchange", StringComparison.OrdinalIgnoreCase) > 0)
										{
											flag = true;
										}
										else
										{
											xmlTextReader.Skip();
										}
									}
								}
							}
							else if (xmlTextReader.NodeType == XmlNodeType.Attribute && string.Equals(xmlTextReader.Name, "Schema", StringComparison.Ordinal) && string.Equals(xmlTextReader.Value, "[Exchange]", StringComparison.OrdinalIgnoreCase))
							{
								flag = true;
							}
						}
					}
				}
				if (stringBuilder != null)
				{
					stringBuilder.Replace(" ", string.Empty);
					badPlanOperators = stringBuilder.ToString(0, Math.Min(stringBuilder.Length, 127));
				}
				result = (flag2 && flag);
			}
			catch (XmlException ex)
			{
				if (ExTraceGlobals.BadPlanDetectionTracer.IsTraceEnabled(TraceType.ErrorTrace))
				{
					ExTraceGlobals.BadPlanDetectionTracer.TraceError(0L, "Exception reading SQL Showplan XML. Type:[{0}] Message:[{1}] StackTrace:[{2}] SQL Showplan XML:[{3}]", new object[]
					{
						ex.GetType(),
						ex.Message,
						ex.StackTrace,
						sqlShowplanXml
					});
				}
				result = false;
			}
			return result;
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x0003CBA4 File Offset: 0x0003ADA4
		private void ReportBadPlan(string sqlShowplanXml, string sqlStatement, string badPlanOperators)
		{
			int num = this.CalculateSQLStatementHash(sqlStatement);
			if (ExTraceGlobals.BadPlanDetectionTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.BadPlanDetectionTracer.TraceDebug(0L, "Bad SQL plan detected. SQL Statement Hash:[{0}] SQL Statement:[{1}] SQL Showplan XML:[{2}] Bad Plan Operators:[{3}]", new object[]
				{
					num,
					sqlStatement,
					sqlShowplanXml,
					badPlanOperators
				});
			}
			bool flag;
			if (BadPlanDetector.ShouldSendWatsonReports && !BadPlanDetector.hashesAlreadyReported.TryGetValue(num, out flag))
			{
				AssemblyName name = Assembly.GetExecutingAssembly().GetName();
				ExWatson.SendGenericWatsonReport("E12", ExWatson.ApplicationVersion.ToString(), ExWatson.AppName, name.Version.ToString(), name.Name, "BadSQLPlan", sqlStatement, num.ToString(), badPlanOperators, sqlShowplanXml);
				BadPlanDetector.hashesAlreadyReported.Add(num, true);
			}
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x0003CC5C File Offset: 0x0003AE5C
		private int CalculateSQLStatementHash(string sqlStatement)
		{
			string text = Regex.Replace(sqlStatement, "\\[pi[0-9]*\\]", "[pi]", RegexOptions.IgnoreCase);
			text = Regex.Replace(text, "pi\\.C[0-9]*", "pi.C", RegexOptions.IgnoreCase);
			text = text.ToUpper();
			return text.GetHashCode();
		}

		// Token: 0x040006B4 RID: 1716
		private static readonly TimeSpan DetectionInterval = TimeSpan.FromHours(1.0);

		// Token: 0x040006B5 RID: 1717
		private static readonly TimeSpan DetectionInitialDelay = TimeSpan.FromMinutes(10.0);

		// Token: 0x040006B6 RID: 1718
		private static readonly List<string> BadPhysicalOperators = new List<string>(new string[]
		{
			"Bitmap",
			"Hash Match",
			"Parallelism",
			"Parameter Table Scan",
			"Remote Delete",
			"Remote Index Scan",
			"Remote Index Seek",
			"Remote Insert",
			"Remote Query",
			"Remote Scan",
			"Remote Update",
			"Table Scan"
		});

		// Token: 0x040006B7 RID: 1719
		private static readonly List<string> BadLogicalOperators = new List<string>(new string[]
		{
			"Bitmap Create",
			"Distribute Streams",
			"Gather Streams",
			"Remote Delete",
			"Remote Index Scan",
			"Remote Index Seek",
			"Remote Insert",
			"Remote Query",
			"Remote Scan",
			"Remote Update",
			"Repartition Streams",
			"Table Scan"
		});

		// Token: 0x040006B8 RID: 1720
		private static readonly KeyValuePair<string, string>[] BadOperatorPairs = new KeyValuePair<string, string>[0];

		// Token: 0x040006B9 RID: 1721
		private static bool shouldSendWatsonReports = false;

		// Token: 0x040006BA RID: 1722
		private static LockFreeDictionary<int, bool> hashesAlreadyReported = new LockFreeDictionary<int, bool>();

		// Token: 0x040006BB RID: 1723
		private readonly StoreDatabase database;
	}
}
