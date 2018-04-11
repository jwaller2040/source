function rotateImage(a) {
    let r = [];
    for (var x = 0; x < a.length; x += 1) {
        let xa = [];
        for (var y = 0; y < a[x].length; y += 1) {
            xa.unshift(a[y][x]);
        }
        r.push(xa);
    }
    return r;
}
