import 'package:flutter_test/flutter_test.dart';
import 'package:student_app/main.dart';

void main() {
  testWidgets('DeptPick app starts successfully', (tester) async {
    await tester.pumpWidget(const DeptPickApp());

    expect(find.text('Welcome to DeptPick'), findsOneWidget);
  });
}
