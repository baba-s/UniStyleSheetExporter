using System.Diagnostics;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine.UIElements;

namespace Kogane.Internal
{
	internal static class StyleSheetExporter
	{
		private const string OUTPUT_PATH = "UniStyleSheetExporter";

		private static readonly string[] m_pathList =
		{
			"StyleSheets/DefaultCommonLight.uss",
			"StyleSheets/DefaultCommonDark.uss",
			"StyleSheets/Generated/DefaultCommonLight.uss.asset",
			"StyleSheets/Generated/DefaultCommonLight_roboto.uss.asset",
			"StyleSheets/Generated/DefaultCommonLight_segoe ui.uss.asset",
			"StyleSheets/Generated/DefaultCommonLight_verdana.uss.asset",
			"StyleSheets/Generated/DefaultCommonDark.uss.asset",
			"StyleSheets/Generated/DefaultCommonDark_roboto.uss.asset",
			"StyleSheets/Generated/DefaultCommonDark_segoe ui.uss.asset",
			"StyleSheets/Generated/DefaultCommonDark_verdana.uss.asset",
			"StyleSheets/Generated/ToolbarLight.uss.asset",
			"StyleSheets/Generated/ToolbarLight_roboto.uss.asset",
			"StyleSheets/Generated/ToolbarLight_segoe ui.uss.asset",
			"StyleSheets/Generated/ToolbarLight_verdana.uss.asset",
			"StyleSheets/Generated/ToolbarDark.uss.asset",
			"StyleSheets/Generated/ToolbarDark_roboto.uss.asset",
			"StyleSheets/Generated/ToolbarDark_segoe ui.uss.asset",
			"StyleSheets/Generated/ToolbarDark_verdana.uss.asset",
		};

		private static readonly MethodInfo WriteStyleSheetMethodInfo = typeof( Editor ).Assembly
			.GetType( "UnityEditor.StyleSheets.StyleSheetToUss" )
			.GetMethod( "WriteStyleSheet", BindingFlags.Static | BindingFlags.Public );

		[MenuItem( "Edit/UniStyleSheetExporter/Export" )]
		private static void Export()
		{
			foreach ( var path in m_pathList )
			{
				var styleSheet = EditorGUIUtility.Load( path ) as StyleSheet;

				if ( styleSheet == null ) continue;

				if ( !Directory.Exists( OUTPUT_PATH ) )
				{
					Directory.CreateDirectory( OUTPUT_PATH );
				}

				var filename   = Path.GetFileName( path ).Replace( ".asset", string.Empty );
				var outputPath = $"{OUTPUT_PATH}/{filename}";
				var parameters = new object[] { styleSheet, outputPath, null };

				WriteStyleSheetMethodInfo.Invoke( null, parameters );
			}

			Process.Start( OUTPUT_PATH );
		}
	}
}