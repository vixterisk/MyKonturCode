using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profiling
{
    class Generator
    {
        public static string GenerateDeclarations()
        {
            var declarations = new StringBuilder();
            var fields = new StringBuilder();
            foreach (var number in Constants.FieldCounts)
            {
                for (int j = number / 2; j < number; j++)
                    fields.Append(String.Format("byte Value{0}; ", j));
                declarations.Append(String.Format("struct S{0} {{ " + fields + @" }}
                                                    class C{0} {{ " + fields + " }} ", number));
            }
            return declarations.ToString();
        }
		
        public static string GenerateArrayRunner()
        {
            var operators = new StringBuilder();
            operators.Append("public class ArrayRunner : IRunner {");
            foreach (var number in Constants.FieldCounts)
            {
                operators.Append(String.Format(@"
                void PC{0}() {{
                   var array = new C{0}[Constants.ArraySize];
                   for (int i = 0; i < Constants.ArraySize; i++) array[i] = new C{0}();
                }}
               void PS{0}() {{ var array = new S{0}[Constants.ArraySize]; }} ", number));
            }
            operators.Append("public void Call(bool isClass, int size, int count) { ");
            foreach (var number in Constants.FieldCounts)
            {
                operators.Append(String.Format(@"
               if (isClass && size == {0}) {{
                   for (int i = 0; i < count; i++) PC{0}(); return;
               }}
               if (!isClass && size == {0}) {{
                   for (int i = 0; i < count; i++) PS{0}(); return;
                }} ", number));
            }
            operators.Append("throw new ArgumentException(); } }");
            return operators.ToString();
        }
			
        public static string GenerateCallRunner()
        {
            var operators = new StringBuilder();
            operators.Append("public class CallRunner : IRunner {");
            foreach (var number in Constants.FieldCounts)
            {
                operators.Append(String.Format(@"
                void PC{0}(C{0} o) {{ }}
               void PS{0}(S{0} o) {{ }} ", number));
            }
            operators.Append("public void Call(bool isClass, int size, int count) { ");
            foreach (var number in Constants.FieldCounts)
            {
                operators.Append(String.Format(@"
               if (isClass && size == {0}) {{
                   var o = new C{0}(); for (int i = 0; i < count; i++) PC{0}(o); return;
               }}
               if (!isClass && size == {0}) {{
                   var o = new S{0}(); for (int i = 0; i < count; i++) PS{0}(o); return;
                }} ", number));
            }
            operators.Append("throw new ArgumentException(); } }");
            return operators.ToString();
        }
    }
}
