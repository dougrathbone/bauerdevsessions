/// Setup Base namespace
var Compression = {
    Demo: {}
};

/**********************************************/
/*********  Run Length Encoding     ***********/
/**********************************************/
(function (demo, undefined) {
    (function (rle, undefined) {
       rle.Context = function(options) {
            return {
                position: 0,
                count: 0,
                pause: options.pause || true,
                text: options.text,
                prev: '',
                run: {
                    symbol: '',
                    start: 0,
                    count: 0,
                    output: ''
                },
                output: '',
                complete: false
            };
        };

        rle.encode = function (context)
        { 
	        while (context.text[context.position]) {
	            if (context.text[context.position] != context.prev) {

			        context.run.symbol = context.prev;
			        context.run.output = context.prev;
		            context.run.count = context.count;
		            context.run.start = context.position - context.count;

		            if (context.position && context.count > 1) {
		                context.output = context.output + context.count;
		                context.run.output = context.run.symbol + context.count;
		            } 

			        context.output = context.output + context.text[context.position];
			        context.prev = context.text[context.position];

                    context.count = 0;
		        }

                context.count++;
		        context.position++;

                if (context.pause) {
                    return null;
                }
	        }
 
	        context.run.symbol = context.prev;
	        context.run.count = context.count;
	        context.run.start = context.position - context.count;

	        if (context.count > 1) {
	            context.output = context.output + context.count;
	            context.run.output = context.run.symbol + context.count;
	        } 

            context.complete = true;
	        return context.output;
        }
    })(demo.RunLengthEncoding = demo.RunLengthEncoding || {});
})(Compression.Demo = Compression.Demo || {});

/**********************************************/
/*********  Static Huffman Code     ***********/
/**********************************************/
(function (demo, undefined) {
    (function (staticHuffman, undefined) {
        staticHuffman.Context = function (options) {
            options = options || { pause: true, text: '' };

            return {
                position: -1,
                symbol: '',
                table: {},
                queue: [],
                pause: options.pause || true,
                text: options.text,
                complete: false,
                codeQueue: []
            };
        };

        function Frequency(symbol) {
            return {
                symbol: symbol,
                count: 1,
                code: ""
            };
        };

        function Node(frequency) {
            return {
                left: null,
                right: null,
                symbol: frequency.symbol,
                count: frequency.count,
                code: ""
            };
        };

        staticHuffman.buildFrequencyTable = function (context) {
            var i;

            context.position++;

            for (i = context.position; i < context.text.length; i++) {
                var symbol = context.text[i];
                context.symbol = symbol;

                if (context.table[symbol]) {
                    context.table[symbol].count++;
                } else {
                    context.table[symbol] = new Frequency(symbol);
                }
                
                if (i < context.text.length -1 && context.pause) {
                    return;
                }
            }

            context.complete = true;
        };

        staticHuffman.buildQueue = function (context) {
            context.queue = [];
            context.complete = false;

            for (var symbol in context.table) {
                context.queue.push(new Node(context.table[symbol]));
            }

            context.queue.sort(sortQueue);
        };

        staticHuffman.buildTree = function (context) {
            var queue = context.queue;


            while (queue.length > 1) {
                var lowest = queue.pop();
                var nextLowest = queue.pop();

                var parent = new Node({ symbol: nextLowest.symbol + lowest.symbol, count: nextLowest.count + lowest.count });
                parent.left = nextLowest;
                parent.right = lowest;
                queue.push(parent);
                queue.sort(sortQueue);

                if (queue.length > 1 && context.pause) {
                    return;
                }
            }

            context.complete = true;
        }
        
        staticHuffman.buildCodes = function (context) {
            var queue = context.queue;

            nextCode(queue[0], "");
        }

        var nextCode = function (node, code) {
            node.code = code;

            if (node.left)
                nextCode(node.left, code + "0");

            if (node.right)
                nextCode(node.right, code + "1");
        }

        var sortQueue = function (a, b) {
            if (a.count < b.count) {
                return 1;
            } else if (a.count > b.count) {
                return -1;
            } else if (a.symbol > b.symbol) {
                return -1;
            } else {
                return 0;
            }
        };

    })(demo.StaticHuffmanCode = demo.StaticHuffmanCode || {});
})(Compression.Demo = Compression.Demo || {});



/**********************************************/
/*********    Arithmetic Coding     ***********/
/**********************************************/
(function (demo, $, undefined) {
    (function (arithmeticCoding, $, undefined) {
        var self = arithmeticCoding;
        
        self.Context = function (options) {
            options = $.extend({}, { pause: true, text: '', table: {} }, options);

            return {
                position: -1,
                symbol: '',
                text: options.text,
                table: options.table,
                high: 1,
                low: 0,
                pause: options.pause,
                complete: false
            };
        };

        self.encode = function (context) {
            var i;

            context.position++;

            for (i = context.position; i < context.text.length; i++) {
                var symbol = context.text[i];
                context.symbol = symbol;

                var codeRange = context.high - context.low;
                context.high = context.low + codeRange * context.table[symbol].high;
                context.low = context.low + codeRange * context.table[symbol].low;
                
                if (i < context.text.length -1 && context.pause) {
                    return;
                }
            }

            context.complete = true;
        };

    })(demo.ArithmeticCoding = demo.ArithmeticCoding || {}, $);
})(Compression.Demo = Compression.Demo || {}, jQuery);


/**********************************************/
/*********    Arithmetic Coding     ***********/
/**********************************************/
(function (demo, $, undefined) {
    (function (lzw, $, undefined) {
        var self = lzw;
               
        self.Context = function (options) {
            options = $.extend({}, { pause: true, text: '' }, options);

            return {
                position: -1,
                w: '',
                k: '',
                text: options.text,
                dictionary: null,
                code: '',
                output: '',
                outputText: '',
                symbol: '',
                index : 256,
                pause: options.pause,
                complete: false
            };
        };
        
        self.encode = function (context) {
            var i;

            if (!context.dictionary) {
                context.dictionary = {};
                for (i = 0; i < context.text.length; i++) {
                    var c = context.text[i];
                    context.dictionary[c] = c;
                }
            }

            context.position++;
            
            for (i = context.position; i < context.text.length; i++) {
                var w = context.code;
                var k = context.text[i];
                var wk = w + k;

                context.w = w;
                context.k = k;
                context.symbol = wk;

                if (context.dictionary[wk]) {
                    context.code = wk;
                    context.output = '';
                } else {
                    context.dictionary[wk] = "<" + (context.index++) + ">";
                    context.output = context.dictionary[w];
                    context.outputText = context.outputText + context.output;
                    context.code = k;
                }
                                
                if (context.pause) {
                    return;
                }
            }

            context.w = context.code;
            context.k = '';
            context.output = context.dictionary[context.w];
            context.outputText = context.outputText + context.output;
            context.code = '';
            context.symbol = '';

            context.complete = true;
        };

    })(demo.Lzw = demo.Lzw || {}, $);
})(Compression.Demo = Compression.Demo || {}, jQuery);
