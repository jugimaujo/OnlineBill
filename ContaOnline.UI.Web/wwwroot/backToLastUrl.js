function backUrl() {
    console.log('234243')

    let pathname = window.location.pathname;

    pathname = pathname.slice(- (pathname.length - 1));
    pathnameList = pathname.split('/');

    pathnameList.forEach((pathname, index) => {
        pathname = Array.from(pathname);

        const hasNumericChar = pathname.some(c => {
            return c in ['1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '-']
        });

        if (hasNumericChar) {
            pathnameList.splice(index, 1);
        }
    });

    if (pathnameList.length == 1) {
        pathname = '';
    }
    else {
        pathnameList = pathnameList.splice(0, 1);
        pathname = pathnameList.join('/')
    }

    pathname = window.location.origin + '/' + pathname;

    window.location.href = pathname;
}
