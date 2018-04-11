// First argument to 'describe' (which is defined by Jasmine) is the testing module that will
// appear in test reports. The second argument is a callback containing the individual tests.

describe("rotateImage", function () {
    // The 'it' function of Jasmine defined an individual test. The first argument is
    // a description of the test that's appended to the module name. Because a module name
    // is typically a noun, like the name of the function being tested, the description for
    // an individual test is typically written in an action-data format.

    it("rotate array 90 degrees", function () {
        // Invoke the unit being tested as necessary
        var a = [[1, 2, 3],[4, 5, 6],[7, 8, 9]];
        var result = rotateImage(a);

        // Check the results; "expect" and toEqual are Jasmine methods.
       
        expect(result).toEqual([[7,4,1],[8,5,2],[9,6,3]]);
    });
});