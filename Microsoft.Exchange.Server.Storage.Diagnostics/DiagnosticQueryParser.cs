using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Diagnostics.Generated;

namespace Microsoft.Exchange.Server.Storage.Diagnostics
{
	// Token: 0x02000016 RID: 22
	public class DiagnosticQueryParser
	{
		// Token: 0x060000AE RID: 174 RVA: 0x00005264 File Offset: 0x00003464
		private DiagnosticQueryParser(string query)
		{
			if (string.IsNullOrEmpty(query))
			{
				throw new DiagnosticQueryParserException(DiagnosticQueryStrings.QueryNull());
			}
			this.parser = new Parser(query);
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000AF RID: 175 RVA: 0x0000528B File Offset: 0x0000348B
		public static string AllColumns
		{
			get
			{
				return "*";
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00005292 File Offset: 0x00003492
		public DiagnosticQueryParser.QueryType Type
		{
			get
			{
				return this.parser.Type;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x0000529F File Offset: 0x0000349F
		public IList<DiagnosticQueryParser.Column> Select
		{
			get
			{
				return this.parser.Select;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x000052AC File Offset: 0x000034AC
		public DiagnosticQueryParser.Context From
		{
			get
			{
				return this.parser.From;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x000052B9 File Offset: 0x000034B9
		public IDictionary<string, string> Set
		{
			get
			{
				return this.parser.Set;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x000052C6 File Offset: 0x000034C6
		public DiagnosticQueryCriteria Where
		{
			get
			{
				return this.parser.Where;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x000052D3 File Offset: 0x000034D3
		public IList<DiagnosticQueryParser.SortColumn> OrderBy
		{
			get
			{
				return this.parser.OrderBy;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x000052E0 File Offset: 0x000034E0
		public bool IsCountQuery
		{
			get
			{
				return this.parser.IsCountQuery;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x000052ED File Offset: 0x000034ED
		public int MaxRows
		{
			get
			{
				return this.parser.MaxRows;
			}
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x000052FA File Offset: 0x000034FA
		public static DiagnosticQueryParser Create(string query)
		{
			return new DiagnosticQueryParser(query);
		}

		// Token: 0x0400008C RID: 140
		private const string AllColumnsCharacter = "*";

		// Token: 0x0400008D RID: 141
		private readonly Parser parser;

		// Token: 0x02000017 RID: 23
		public enum QueryType
		{
			// Token: 0x0400008F RID: 143
			Unspecified,
			// Token: 0x04000090 RID: 144
			Select,
			// Token: 0x04000091 RID: 145
			Update,
			// Token: 0x04000092 RID: 146
			Insert,
			// Token: 0x04000093 RID: 147
			Delete
		}

		// Token: 0x02000018 RID: 24
		public struct SortColumn : IEquatable<DiagnosticQueryParser.SortColumn>
		{
			// Token: 0x060000B9 RID: 185 RVA: 0x00005302 File Offset: 0x00003502
			public SortColumn(string name)
			{
				this = new DiagnosticQueryParser.SortColumn(name, true);
			}

			// Token: 0x060000BA RID: 186 RVA: 0x0000530C File Offset: 0x0000350C
			public SortColumn(string name, bool asc)
			{
				this.Name = name;
				this.Ascending = asc;
				this.hashCode = (this.Name.GetHashCode() ^ this.Ascending.GetHashCode());
			}

			// Token: 0x060000BB RID: 187 RVA: 0x00005339 File Offset: 0x00003539
			public override int GetHashCode()
			{
				return this.hashCode;
			}

			// Token: 0x060000BC RID: 188 RVA: 0x00005344 File Offset: 0x00003544
			public override bool Equals(object obj)
			{
				if (obj is DiagnosticQueryParser.SortColumn)
				{
					DiagnosticQueryParser.SortColumn sortColumn = (DiagnosticQueryParser.SortColumn)obj;
					return this.Equals(sortColumn);
				}
				return false;
			}

			// Token: 0x060000BD RID: 189 RVA: 0x0000536C File Offset: 0x0000356C
			public bool Equals(DiagnosticQueryParser.SortColumn sortColumn)
			{
				return this.Name.Equals(sortColumn.Name) && this.Ascending.Equals(sortColumn.Ascending);
			}

			// Token: 0x04000094 RID: 148
			public readonly string Name;

			// Token: 0x04000095 RID: 149
			public readonly bool Ascending;

			// Token: 0x04000096 RID: 150
			private readonly int hashCode;
		}

		// Token: 0x02000019 RID: 25
		public class Context : IEquatable<DiagnosticQueryParser.Context>
		{
			// Token: 0x060000BE RID: 190 RVA: 0x000053A4 File Offset: 0x000035A4
			private Context(string database, string schema, DiagnosticQueryParser.TableInfo table)
			{
				this.database = (database ?? string.Empty);
				this.schema = (schema ?? string.Empty);
				this.table = (table ?? DiagnosticQueryParser.TableInfo.Create(string.Empty));
			}

			// Token: 0x17000050 RID: 80
			// (get) Token: 0x060000BF RID: 191 RVA: 0x000053E1 File Offset: 0x000035E1
			public string Database
			{
				get
				{
					return this.database;
				}
			}

			// Token: 0x17000051 RID: 81
			// (get) Token: 0x060000C0 RID: 192 RVA: 0x000053E9 File Offset: 0x000035E9
			public string Schema
			{
				get
				{
					return this.schema;
				}
			}

			// Token: 0x17000052 RID: 82
			// (get) Token: 0x060000C1 RID: 193 RVA: 0x000053F1 File Offset: 0x000035F1
			public DiagnosticQueryParser.TableInfo Table
			{
				get
				{
					return this.table;
				}
			}

			// Token: 0x060000C2 RID: 194 RVA: 0x000053F9 File Offset: 0x000035F9
			public static DiagnosticQueryParser.Context Create(string database, string schema, DiagnosticQueryParser.TableInfo table)
			{
				return new DiagnosticQueryParser.Context(database, schema, table);
			}

			// Token: 0x060000C3 RID: 195 RVA: 0x00005403 File Offset: 0x00003603
			public static DiagnosticQueryParser.Context Create(string database, DiagnosticQueryParser.TableInfo table)
			{
				return new DiagnosticQueryParser.Context(database, null, table);
			}

			// Token: 0x060000C4 RID: 196 RVA: 0x0000540D File Offset: 0x0000360D
			public static DiagnosticQueryParser.Context Create(DiagnosticQueryParser.TableInfo table)
			{
				return new DiagnosticQueryParser.Context(null, null, table);
			}

			// Token: 0x060000C5 RID: 197 RVA: 0x00005418 File Offset: 0x00003618
			public override string ToString()
			{
				if (this.readableFormat == null)
				{
					StringBuilder stringBuilder = new StringBuilder();
					string[] array = new string[]
					{
						this.Database,
						this.Schema,
						this.Table.Name
					};
					foreach (string text in array)
					{
						if (!string.IsNullOrEmpty(text))
						{
							stringBuilder.AppendFormat("{0}[{1}]", (stringBuilder.Length > 0) ? "." : string.Empty, text);
						}
					}
					if (this.Table.Parameters != null)
					{
						string arg = string.Empty;
						stringBuilder.Append("(");
						foreach (string arg2 in this.table.Parameters)
						{
							stringBuilder.AppendFormat("{0}{1}", arg, arg2);
							arg = ", ";
						}
						stringBuilder.Append(")");
					}
					this.readableFormat = stringBuilder.ToString();
				}
				return this.readableFormat;
			}

			// Token: 0x060000C6 RID: 198 RVA: 0x00005524 File Offset: 0x00003724
			public override int GetHashCode()
			{
				if (this.hashCode == null)
				{
					this.hashCode = new int?(this.Database.GetHashCode() ^ this.Schema.GetHashCode() ^ this.Table.GetHashCode());
				}
				return this.hashCode.Value;
			}

			// Token: 0x060000C7 RID: 199 RVA: 0x00005578 File Offset: 0x00003778
			public override bool Equals(object obj)
			{
				DiagnosticQueryParser.Context context = obj as DiagnosticQueryParser.Context;
				return this.Equals(context);
			}

			// Token: 0x060000C8 RID: 200 RVA: 0x00005593 File Offset: 0x00003793
			public bool Equals(DiagnosticQueryParser.Context context)
			{
				return context != null && (this.Database.Equals(context.Database) && this.Schema.Equals(context.Schema)) && this.Table.Equals(context.Table);
			}

			// Token: 0x04000097 RID: 151
			private readonly string database;

			// Token: 0x04000098 RID: 152
			private readonly string schema;

			// Token: 0x04000099 RID: 153
			private readonly DiagnosticQueryParser.TableInfo table;

			// Token: 0x0400009A RID: 154
			private int? hashCode;

			// Token: 0x0400009B RID: 155
			private string readableFormat;
		}

		// Token: 0x0200001A RID: 26
		public class TableInfo : IEquatable<DiagnosticQueryParser.TableInfo>
		{
			// Token: 0x060000C9 RID: 201 RVA: 0x000055D3 File Offset: 0x000037D3
			private TableInfo(string name, List<string> parameters)
			{
				this.name = (name ?? string.Empty);
				this.parameters = ((parameters == null) ? null : parameters.ToArray());
			}

			// Token: 0x17000053 RID: 83
			// (get) Token: 0x060000CA RID: 202 RVA: 0x000055FD File Offset: 0x000037FD
			public string Name
			{
				get
				{
					return this.name;
				}
			}

			// Token: 0x17000054 RID: 84
			// (get) Token: 0x060000CB RID: 203 RVA: 0x00005605 File Offset: 0x00003805
			public string[] Parameters
			{
				get
				{
					return this.parameters;
				}
			}

			// Token: 0x060000CC RID: 204 RVA: 0x0000560D File Offset: 0x0000380D
			public static DiagnosticQueryParser.TableInfo Create(string name)
			{
				return new DiagnosticQueryParser.TableInfo(name, null);
			}

			// Token: 0x060000CD RID: 205 RVA: 0x00005616 File Offset: 0x00003816
			public static DiagnosticQueryParser.TableInfo Create(string name, List<string> parameters)
			{
				return new DiagnosticQueryParser.TableInfo(name, parameters);
			}

			// Token: 0x060000CE RID: 206 RVA: 0x00005620 File Offset: 0x00003820
			public override int GetHashCode()
			{
				if (this.hashCode == null)
				{
					this.hashCode = new int?(this.Name.GetHashCode());
					if (this.Parameters != null)
					{
						foreach (string text in this.Parameters)
						{
							if (text != null)
							{
								this.hashCode ^= text.GetHashCode();
							}
						}
					}
				}
				return this.hashCode.Value;
			}

			// Token: 0x060000CF RID: 207 RVA: 0x000056B8 File Offset: 0x000038B8
			public override bool Equals(object obj)
			{
				DiagnosticQueryParser.TableInfo tableInfo = obj as DiagnosticQueryParser.TableInfo;
				return this.Equals(tableInfo);
			}

			// Token: 0x060000D0 RID: 208 RVA: 0x000056D4 File Offset: 0x000038D4
			public bool Equals(DiagnosticQueryParser.TableInfo tableInfo)
			{
				if (tableInfo == null)
				{
					return false;
				}
				if (!this.Name.Equals(tableInfo.Name))
				{
					return false;
				}
				if (this.Parameters == null != (tableInfo.Parameters == null))
				{
					return false;
				}
				if (this.Parameters.Length != tableInfo.Parameters.Length)
				{
					return false;
				}
				for (int i = 0; i < this.Parameters.Length; i++)
				{
					if (!this.Parameters[i].Equals(tableInfo.Parameters[i]))
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x0400009C RID: 156
			private readonly string name;

			// Token: 0x0400009D RID: 157
			private readonly string[] parameters;

			// Token: 0x0400009E RID: 158
			private int? hashCode;
		}

		// Token: 0x0200001B RID: 27
		public class Column : IEquatable<DiagnosticQueryParser.Column>
		{
			// Token: 0x060000D1 RID: 209 RVA: 0x00005752 File Offset: 0x00003952
			protected Column(string identifier, bool subtractor)
			{
				this.identifier = identifier;
				this.subtractor = subtractor;
			}

			// Token: 0x17000055 RID: 85
			// (get) Token: 0x060000D2 RID: 210 RVA: 0x00005768 File Offset: 0x00003968
			public string Identifier
			{
				get
				{
					return this.identifier;
				}
			}

			// Token: 0x17000056 RID: 86
			// (get) Token: 0x060000D3 RID: 211 RVA: 0x00005770 File Offset: 0x00003970
			public bool IsSubtraction
			{
				get
				{
					return this.subtractor;
				}
			}

			// Token: 0x060000D4 RID: 212 RVA: 0x00005778 File Offset: 0x00003978
			public static DiagnosticQueryParser.Column Create(string identifier, bool subtractor)
			{
				return new DiagnosticQueryParser.Column(identifier, subtractor);
			}

			// Token: 0x060000D5 RID: 213 RVA: 0x00005784 File Offset: 0x00003984
			public override bool Equals(object obj)
			{
				DiagnosticQueryParser.Column column = obj as DiagnosticQueryParser.Column;
				return this.Equals(column);
			}

			// Token: 0x060000D6 RID: 214 RVA: 0x0000579F File Offset: 0x0000399F
			public override int GetHashCode()
			{
				return this.Identifier.GetHashCode();
			}

			// Token: 0x060000D7 RID: 215 RVA: 0x000057AC File Offset: 0x000039AC
			public bool Equals(DiagnosticQueryParser.Column column)
			{
				return column != null && this.Identifier.Equals(column.Identifier) && this.IsSubtraction.Equals(column.IsSubtraction);
			}

			// Token: 0x0400009F RID: 159
			private readonly string identifier;

			// Token: 0x040000A0 RID: 160
			private readonly bool subtractor;
		}

		// Token: 0x0200001C RID: 28
		public class Processor : DiagnosticQueryParser.Column, IEquatable<DiagnosticQueryParser.Processor>
		{
			// Token: 0x060000D8 RID: 216 RVA: 0x000057EC File Offset: 0x000039EC
			private Processor(string identifier, IList<DiagnosticQueryParser.Column> arguments) : base(identifier, false)
			{
				this.arguments = arguments;
			}

			// Token: 0x17000057 RID: 87
			// (get) Token: 0x060000D9 RID: 217 RVA: 0x000057FD File Offset: 0x000039FD
			public IList<DiagnosticQueryParser.Column> Arguments
			{
				get
				{
					return this.arguments;
				}
			}

			// Token: 0x060000DA RID: 218 RVA: 0x00005805 File Offset: 0x00003A05
			public static DiagnosticQueryParser.Column Create(string identifier)
			{
				return DiagnosticQueryParser.Processor.Create(identifier, Array<DiagnosticQueryParser.Column>.Empty);
			}

			// Token: 0x060000DB RID: 219 RVA: 0x00005812 File Offset: 0x00003A12
			public static DiagnosticQueryParser.Column Create(string identifier, IList<DiagnosticQueryParser.Column> arguments)
			{
				return new DiagnosticQueryParser.Processor(identifier, arguments);
			}

			// Token: 0x060000DC RID: 220 RVA: 0x0000581C File Offset: 0x00003A1C
			public override bool Equals(object obj)
			{
				DiagnosticQueryParser.Processor processor = obj as DiagnosticQueryParser.Processor;
				return this.Equals(processor);
			}

			// Token: 0x060000DD RID: 221 RVA: 0x00005838 File Offset: 0x00003A38
			public override int GetHashCode()
			{
				if (this.hashCode == null)
				{
					this.hashCode = new int?(base.Identifier.GetHashCode());
					if (this.Arguments != null)
					{
						foreach (DiagnosticQueryParser.Column column in this.Arguments)
						{
							if (column != null)
							{
								this.hashCode ^= column.GetHashCode();
							}
						}
					}
				}
				return this.hashCode.Value;
			}

			// Token: 0x060000DE RID: 222 RVA: 0x000058F4 File Offset: 0x00003AF4
			public bool Equals(DiagnosticQueryParser.Processor processor)
			{
				if (processor == null)
				{
					return false;
				}
				if (!base.Identifier.Equals(processor.Identifier))
				{
					return false;
				}
				if (this.Arguments == null != (processor.Arguments == null))
				{
					return false;
				}
				if (this.Arguments == null)
				{
					return true;
				}
				if (this.Arguments.Count != processor.Arguments.Count)
				{
					return false;
				}
				for (int i = 0; i < this.Arguments.Count; i++)
				{
					if (!this.Arguments[i].Equals(processor.Arguments[i]))
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x040000A1 RID: 161
			private readonly IList<DiagnosticQueryParser.Column> arguments;

			// Token: 0x040000A2 RID: 162
			private int? hashCode;
		}
	}
}
